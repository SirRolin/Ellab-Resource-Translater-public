using Ellab_Resource_Translater.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public class DBBatchCommandWrapper(DbBatchCommand command) : IDBparameterable
    {
        private readonly DbBatchCommand command = command;

        public DbParameterCollection Parameters => command.Parameters;

        public DbParameter CreateParameter()
        {
            return command.CreateParameter();
        }

        public static implicit operator DbBatchCommand(DBBatchCommandWrapper cbdw) => cbdw.command;
        public static implicit operator DBBatchCommandWrapper(DbBatchCommand ccd) => new(ccd);
    }
}
