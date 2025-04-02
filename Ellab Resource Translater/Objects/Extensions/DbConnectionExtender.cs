using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Ellab_Resource_Translater.Util;

namespace Ellab_Resource_Translater.Objects.Extensions
{
    internal static class DbConnectionExtender
    {
        public static bool CanMultiResult(this DbConnection conn)
        {
            return !conn.ConnectionString.Contains("MultipleActiveResultSets=False");
        }

        private static readonly ConcurrentDictionary<DbConnection, WeakReference<SemaphoreSlim>> _semaphores = [];

        private static SemaphoreSlim GetInstanceSemaphore(DbConnection db)
        {
            return _semaphores.GetOrAdd(db, _ => new WeakReference<SemaphoreSlim>(new SemaphoreSlim(1, 1)))
                              .TryGetTarget(out var sem) ? sem : new SemaphoreSlim(1, 1);
        }

        public static async Task ThreadSafeAsyncFunction(this DbConnection conn, Action<DbConnection> query)
        {
            if (conn == null)
                return;

            // In case we can have multiple Result Sets
            if (conn.CanMultiResult())
            {
                query.Invoke(conn);
                return;
            }

            // Threadsafe in case we can't
            SemaphoreSlim semaphore = GetInstanceSemaphore(conn);
            await semaphore.WaitAsync();
            try
            {
                query.Invoke(conn);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static void WaitForOpen(this DbConnection conn)
        {
            conn.WaitForOpen(() => { });
        }
        public static void WaitForOpen(this DbConnection conn, Action ConnectionBroke)
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

                if (!conn.State.HasFlag(ConnectionState.Open))
                    Task.Delay(Config.Get().checkDelay).Wait();
            }
        }

        public static void WaitForFinish(this DbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
                conn.WaitForOpen();

            while (conn.State.HasFlag(ConnectionState.Executing) || conn.State.HasFlag(ConnectionState.Fetching))
            {
                Task.Delay(Config.Get().checkDelay).Wait();
            }
        }

        public static void TryClose(this DbConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
                conn.CloseAsync();
        }
    }
}
