using Ellab_Resource_Translater.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public class DBCommandWrapper(DbCommand command) : IDBparameterable
    {
        private readonly DbCommand command = command;

        public DbParameterCollection Parameters => command.Parameters;

        public DbParameter CreateParameter()
        {
            return command.CreateParameter();
        }

        public static implicit operator DbCommand(DBCommandWrapper cdw) => cdw.command;
        public static implicit operator DBCommandWrapper(DbCommand cd) => new(cd);
    }
}
