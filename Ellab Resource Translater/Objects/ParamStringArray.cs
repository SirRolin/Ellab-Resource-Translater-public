using System.Data;
using System.Data.Common;

namespace Ellab_Resource_Translater.Objects
{
    /// <summary>
    /// A easy way to handle an array to a command.
    /// </summary>
    class ParamStringArray
    {

        private (string param, string value)[] Array { get; }
        private string Parameter { get; }

        /// <summary>
        /// Constructs an object to easily insert into a command.<br/>
        /// In the commandText insert this where the array are placed, like so: {langs}<br/>
        /// Afterwards use langs.<see cref="AddParam(DbCommand)"/> to apply the parameters.
        /// </summary>
        /// <param name="baseParamName">Name for the param combined with the index of the array.</param>
        /// <param name="valStrings">Array/List of params to add</param>
        /// <param name="extra">Extra things to add to the params, without modifying <paramref name="valStrings"/></param>
        public ParamStringArray(string baseParamName, IEnumerable<string> valStrings, params string[] extra)
        {
            string[] union = [.. valStrings, .. extra];
            Array = [.. union.Select((val, ite) => (string.Concat(baseParamName, ite), val))];
            Parameter = string.Join(", ", Array.Select(x => x.param));
        }

        /// <summary>
        /// Creates and Adds the Parameters to the given DbCommand.
        /// </summary>
        public void AddParam(DbCommand dbc)
        {
            foreach (var (param, value) in Array)
            {
                var cp = dbc.CreateParameter();
                cp.ParameterName = param;
                cp.Value = value;
                cp.DbType = DbType.String;
                dbc.Parameters.Add(cp);
            }
        }

        /// <summary>
        /// Gives the parameter string. If you want a full string of this object use <see cref="GetDebugString()"/> instead.
        /// </summary>
        /// <returns></returns>
        override
        public string ToString()
        {
            return Parameter;
        }

        public string GetDebugString()
        {
            return string.Concat(Parameter, 
                "|", 
                string.Join(", ", 
                    Array.Select(item => string.Concat(item.param, "=", item.value))
                    )
                );
        }
    }
}
