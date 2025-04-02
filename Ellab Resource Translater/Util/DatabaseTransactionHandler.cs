using Ellab_Resource_Translater.Enums;
using Ellab_Resource_Translater.Interfaces;
using Ellab_Resource_Translater.Objects;
using Ellab_Resource_Translater.Objects.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Azure;
using Mysqlx.Crud;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ellab_Resource_Translater.Util
{
    /// <summary>
    /// Starts a Transaction and calls <paramref name="onTransactionStart"/>,<br/>
    /// <see cref="DataRow"/>s in the <see cref="DataTable"/> provided with <see cref="AddInsert(DataTable)"/><br/>
    /// are fed into <paramref name="addParameters"/> to give commands with <paramref name="commandText"/> it's parameters<br/>
    /// This uses <paramref name="inserters"/> threads. runs in parallel if not positive.
    /// </summary>
    /// <param name="source">Token to cancel with.</param>
    /// <param name="onTransactionStart">Action Run right after transaction start.</param>
    /// <param name="commandText">SQL Query</param>
    /// <param name="addParameters">How we add the parameters to the command.</param>
    /// <param name="inserters">How many threads to use, runs in parallel if not positive.</param>
    public class DatabaseTransactionHandler(CancellationTokenSource source, Action<DbConnection, DbTransaction?> onTransactionStart, string commandText, Action<DataRow, IDBparameterable> addParameters, int inserters = 4)
    {
        private readonly ConcurrentQueue<DataTable> insertToDatabaseTasks = [];
        private readonly CancellationToken token = source.Token;
        private int insertsPending = 0;
        private bool _waitTillStopped = false;
        private readonly object LockObject = new();

        /// <summary>
        /// Threadsafely inserts the table <paramref name="dt"/> to the internal queue.<br/>
        /// use <see cref="StartCommands(ConnectionProvider, Label, ListView, Func{DataTable, string}, bool)"/>.
        /// </summary>
        /// <param name="dt"><see cref="DataTable"/> to extract rows from and create the parameters for the command.</param>
        public void AddInsert(DataTable dt)
        {
            Interlocked.Increment(ref insertsPending);
            insertToDatabaseTasks.Enqueue(dt);
            Interlocked.Decrement(ref insertsPending);
        }

        /// <summary>
        /// Starts processing everything feeded into it.
        /// </summary>
        /// <param name="connProv">to generate a <see cref="DbConnection"/> and automatically close it.</param>
        /// <param name="progresText"><see cref="Label"/> of which to update progress.</param>
        /// <param name="listView"><see cref="ListView"/> of which to show current processes</param>
        /// <param name="getResourceName">Function that takes the <see cref="DataTable"/> and returns a the process shown.</param>
        /// <param name="waitTillStopped">When <see langword="false"/> will only continue until there's no current or pending inserts.<br/>
        /// When <see langword="true"/> will also wait for <see cref="NoMoreInserts"/> to be called.</param>
        public void StartCommands(ConnectionProvider connProv, Label progresText, ListView listView, Func<DataTable, string> getResourceName, bool waitTillStopped = false)
        {
            _waitTillStopped = waitTillStopped;
            using var dce = connProv.Get();

            bool process(DbConnection dce, DbTransaction? transAct, DataTable item, Action<int, int> updater)
            {
                if (token.IsCancellationRequested)
                    return false;
                else
                    BuildAndExecute(dce, item, transAct, updater);
                return true;
            }

            void execution(DbTransaction? transAct)
            {
                onTransactionStart.Invoke(dce, transAct);
                int currentProcessed = -1;
                // int maxProcesses = insertToDatabaseTasks.Count;
                // Doing this so we don't have to pass both a int ref and a Label ref
                void updateProgresText()
                {
                    var progress = Interlocked.Increment(ref currentProcessed);
                    FormUtils.LabelTextUpdater(progresText, "Inserting ", progress, " out of ", (insertToDatabaseTasks.Count + progress), " into database.");
                }
                updateProgresText();
                ExecutionHandler.Execute(inserters, insertToDatabaseTasks.Count, (i) =>
                {
                    while (insertToDatabaseTasks.TryDequeue(out var dt) || insertsPending > 0 || _waitTillStopped)
                    {
                        // if more are pending
                        if (dt != null)
                        {
                            if (token.IsCancellationRequested)
                                break;
                            string resource = getResourceName.Invoke(dt);
                            FormUtils.ShowOnListWhileProcessing((s) => s,
                                updateProgresText,
                                listView,
                                resource,
                                (ListViewItem lvi) => {
                                    try
                                    {
                                        process(dce, transAct, dt, (cur, outOf) => listView.Invoke(() =>
                                        {
                                            lvi.Text = string.Concat(resource, " (", cur, " / ", outOf, ")");
                                        }));
                                    }
                                    catch (OperationCanceledException)
                                    {
                                        // Operation Cancelled by user
                                    }
                                    return true;
                                });
                            if (token.IsCancellationRequested)
                                break;
                        } else
                        {
                            Task.Delay(Config.Get().checkDelay).Wait();
                        }
                    }
                });
            }

            dce.WaitForOpen(source.Cancel);
            TryScopedTransaction(dce: dce,
                                execution: execution,
                                token: token);

            dce.WaitForFinish();
        }

        /// <summary>
        /// Only does something if <see cref="StartCommands(ConnectionProvider, Label, ListView, Func{DataTable, string}, bool)"/> was called with <see langword="true"/> for <see langword="waitTillStopped"/><br/>
        /// when no more <see cref="DataTable"/>s are available to insert.
        /// </summary>
        /// <returns>Previous State of waiting</returns>
        public bool NoMoreInserts()
        {
            bool output = _waitTillStopped;
            _waitTillStopped = false;
            return output;
        }

        private void BuildAndExecute(DbConnection DBCon, DataTable dataTable, DbTransaction? trans, Action<int,int> update)
        {
            bool batchFailed = false;
            Task PreviousTask = Task.CompletedTask;
            // Initial Report of Progress.
            var i = 0;
            update(i, dataTable.Rows.Count);

            // Upload to the Database
            if (DBCon.CanCreateBatch)
            {

                // Since ValSuite has tousands of entries in some files, I have limited it to ROWS_AT_ONCE at a time to not trigger the timeout.
                const int ROWS_AT_ONCE = 500;
                for (int rowIte = 0; rowIte < dataTable.Rows.Count; rowIte += ROWS_AT_ONCE)
                {
                    /// Must not use using - This is due to trying to execute while preparing the next batch command.
                    var s = DBCon.CreateBatch();
                    if (trans != null)
                        s.Transaction = trans;

                    for (int innerRowIte = rowIte; innerRowIte < (rowIte + ROWS_AT_ONCE) && innerRowIte < dataTable.Rows.Count; innerRowIte++)
                    {
                        DataRow row = dataTable.Rows[innerRowIte];
                        var c = s.CreateBatchCommand();
                        c.CommandText = commandText;
                        addParameters(row, (DBBatchCommandWrapper)c);
                        s.BatchCommands.Add(c);

                        // Report Progress
                        Interlocked.Increment(ref i);
                        update(i, dataTable.Rows.Count);

                        token.ThrowIfCancellationRequested();
                    }
                    PreviousTask.Wait();
                    if (!token.IsCancellationRequested)
                    {
                        PreviousTask = Task.Run(() =>
                        {
                            Exe(ref batchFailed, s);
                        });
                    }
                    if (batchFailed)
                    {
                        break;
                    }
                }
                PreviousTask.Wait();
            }
            else
            {
                batchFailed = true;
            }
            if (batchFailed)
            {
                Ref<int> refI = new(0);
                PreviousTask = Task.CompletedTask;
                foreach (DataRow row in dataTable.Rows)
                {
                    DbCommand command = DBCon.CreateCommand();
                    command.CommandText = commandText;

                    if (trans != null)
                        command.Transaction = trans;

                    DBCommandWrapper wrappedCommand = command;

                    addParameters(row, wrappedCommand);

                    // We run it Asyncroniously so that we can prepare the next Task before we run it.
                    // Otherwise the execute will tell us "ExecuteNonQuery Requires an open and available connection, connection is open".
                    PreviousTask.Wait();

                    // Check if user wants to cancel
                    token.ThrowIfCancellationRequested();

                    PreviousTask = Task.Run(() =>
                    {
                        DBCon.WaitForOpen();
                        CommandExecuteElseMessage(dataTable, DBCon, command);

                        var ii = Interlocked.Increment(ref i);
                        update(ii, dataTable.Rows.Count);
                    });
                }
            }

            // Wait for the last task to be done.
            PreviousTask.Wait();
        }

        private void Exe(ref bool batchFailed, DbBatch s)
        {
            try
            {
                lock (this.LockObject)
                {
                    s.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // If Cancelled we know why the error happened.
                if(!source.IsCancellationRequested)
                    Debug.WriteLine(e.Message);
                batchFailed = true;
            }
            finally
            {
                // Manually Disposing of it instead of using, well... using, cause otherwise it gets disposed off before it gets to execute.
                s.Dispose();
            }
        }

        private void CommandExecuteElseMessage(DataTable dataTable, DbConnection dce, DbCommand command)
        {
            int tries = 0;
            Exception? e = null;
            while (tries < 2)
            {
                try
                {
                    dce.WaitForOpen();

                    command.ExecuteNonQuery();
                    break;
                }
                catch(Exception ee)
                {
                    e = ee;
                    tries++;
                    Task.Delay(Config.Get().checkDelay * 5).Wait();
                }
                if (tries == 2)
                {
                    Cancel();
                    MessageBox.Show("failed to upload to the database:" + e.Message);
                }
            }
        }

        public void Cancel()
        {
            source.Cancel();
        }

        public static bool TryGetTransactionScope(DbConnection connection, out TransactionScope? transactionScope)
        {
            ConnType dbType = DBStringHandler.DetectType(connection.ConnectionString);
            switch (dbType)
            {
                case ConnType.MSSql:
                    transactionScope = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                        TransactionScopeAsyncFlowOption.Enabled);
                    break;

                case ConnType.PostgreSql:
                    transactionScope = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                        TransactionScopeAsyncFlowOption.Enabled);
                    break;

                default:
                    transactionScope = null;
                    return false;
            }
            return true;
        }

        /// <summary>
        /// if the connection allows transaction to share multiple connections (using DTC) <paramref name="execution"/> will be called with <see langword="null"/>.<br/>
        /// if DTC is not supported or it fails, the <see cref="DbTransaction"/> which only supports 1 connection.
        /// </summary>
        /// <param name="dce">connection wrapped in my extension class.</param>
        /// <param name="execution"></param>
        /// <param name="token"><see cref="CancellationToken"/> to cancel with the process.</param>
        /// <returns>DTC supported? <see langword="true"/>, otherwise <see langword="false"/></returns>
        public static bool TryScopedTransaction(DbConnection dce, Action<DbTransaction?> execution, CancellationToken token)
        {
            try
            {
                if (TryGetTransactionScope(dce, out TransactionScope? scope))
                {
                    using var scop = scope;
                    execution(null);
                    if (!token.IsCancellationRequested)
                        scop?.Complete();
                }
                else
                {
                    throw new Exception("Transcope don't work, using DbTransaction instead.");
                }
                return true;
            }
            catch (Exception)
            {
                using DbTransaction dbTrans = dce.BeginTransaction();
                try
                {
                    execution(dbTrans);
                    
                    dce.WaitForFinish();

                    if (token.IsCancellationRequested)
                        dbTrans.Rollback();
                    else
                        dbTrans.Commit();
                }
                catch (Exception)
                {
                    dbTrans.Rollback();
                }
            }
            return false;
        }
    }
}
