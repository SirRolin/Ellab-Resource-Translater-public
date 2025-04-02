using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows.Input;

namespace Ellab_Resource_Translater.Objects.Extensions
{
    public static class DbCommandExt
    {
        public static void EllabAddParameter(this DbCommand dbCommand, string paramName, string value)
        {
            var param = dbCommand.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            param.DbType = DbType.String;
            dbCommand.Parameters.Add(param);
        }

        public static void EllabAddParameters(this DbCommand dbCommand, string paramName, IEnumerable<string> values)
        {
            var iterator = values.GetEnumerator();
            int i = 0;
            while (iterator.MoveNext())
            {
                dbCommand.EllabAddParameter(paramName + i, iterator.Current);
                i++;
            }
        }

        public static string EllabParametiseIterable(string baseParamName, IEnumerable<string> values)
        {
            var paramers = values.Select((s, i) => baseParamName + i).ToArray();
            return string.Join(", ", paramers);
        }
    }
}
