using Ellab_Resource_Translater.Objects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Util
{
    /// <summary>
    /// Provides connections with the <paramref name="connectionString"/> provided and keeps reference to them until they are closed.<br/>
    /// You can call Dispose on this to Dispose of all connections provided.
    /// </summary>
    /// <remarks>
    /// This is Thread-safe.
    /// </remarks>
    /// <param name="connectionString">a string supported by <see cref="DBStringHandler.CreateDbConnection(string)"/>, Types can be seen in <see cref="Enums.ConnType"/></param>
    public class ConnectionProvider : IDisposable
    {
        private readonly List<DbConnection> dces = [];
        private readonly object lockObject = new();
        private bool _isDisposed = false;
        private string connectionString;

        public ConnectionProvider()
        {
            _isDisposed = true;
            connectionString = "";
        }
        public ConnectionProvider(string connectionString) => this.connectionString = connectionString;



        /// <summary>
        /// Disposes of all connections this has provided.
        /// </summary>
        /// <remarks>
        /// Can still provide more <see cref="DbConnection"/>s.
        /// </remarks>
        public void Dispose()
        {
            _isDisposed = true;
            connectionString = "";
            lock (this.lockObject)
            {
                // Get rid of all active connections
                while (dces.Count > 0)
                {
                    dces[0].Dispose();
                }
            }
        }

        public bool isDisposed() => _isDisposed;

        public DbConnection Get()
        {
            DbConnection dce = DBStringHandler.CreateDbConnection(connectionString);
            lock (this.lockObject)
            {
                dces.Add(dce);
            }
            dce.Disposed += (s, e) =>
            {
                lock (this.lockObject)
                {
                    dces.Remove(dce);
                }
            };
            return dce;
        }
    }
}
