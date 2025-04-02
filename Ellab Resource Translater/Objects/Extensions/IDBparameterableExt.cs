using Ellab_Resource_Translater.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects.Extensions
{

    internal static class IDBParameterableExt
    {
        public static void AddParam(this IDBparameterable c, DataRow row, string name, DbType dbType)
        {
            var paramComment = c.CreateParameter();
            paramComment.ParameterName = "@" + name;
            paramComment.Value = row[name];
            paramComment.DbType = dbType;
            c.Parameters.Add(paramComment);
        }
    }
}
