using Ellab_Resource_Translater.Objects.Extensions;
using Ellab_Resource_Translater.Util;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    /// <summary>
    /// a wrapping class to hold a ADO.net connection.
    /// </summary>
    /// <remarks>
    /// Obsolete due to <see cref="DbConnectionExtender"/>
    /// </remarks>
    [Obsolete]
    internal class DbConnectionExtension : IDisposable
    {
        public DbConnectionExtension(DbConnection connection)
        {
            conn = connection;
            canMultiResult = !conn.ConnectionString.Contains("MultipleActiveResultSets=False");
        }
        public DbConnection conn;
        private bool isDisposed = false;
        public bool canMultiResult;

        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task ThreadSafeAsyncFunction(Action<DbConnection> query)
        {
            if (conn == null)
                return;

            // In case we can have multiple Result Sets
            if (canMultiResult)
            {
                query.Invoke(conn);
                return;
            }

            // Threadsafe in case we can't
            await _semaphore.WaitAsync();
            try
            {
                query.Invoke(conn);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void WaitForOpen()
        {
            WaitForOpen(() => { });
        }
        public void WaitForOpen(Action ConnectionBroke)
        {
            // Make sure the connection is open before we proceed
            while (!conn.State.HasFlag(ConnectionState.Open))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                else if (conn.State.HasFlag(ConnectionState.Broken))
                {
                    ConnectionBroke();
                    break;
                }

                if(!conn.State.HasFlag(ConnectionState.Open))
                    Task.Delay(Config.Get().checkDelay).Wait();
            }
        }

        public void WaitForFinish()
        {
            if (conn.State == ConnectionState.Closed)
                WaitForOpen();

            while (conn.State.HasFlag(ConnectionState.Executing) || conn.State.HasFlag(ConnectionState.Fetching))
            {
                Task.Delay(Config.Get().checkDelay).Wait();
            }
        }

        public void TryClose()
        {
            if (conn != null && conn.State != ConnectionState.Closed)
                conn.CloseAsync();
        }

        public void Dispose()
        {
            if(isDisposed) return;
            isDisposed = true;
            conn?.Dispose();
            _semaphore.Dispose();
        }
    }
}
