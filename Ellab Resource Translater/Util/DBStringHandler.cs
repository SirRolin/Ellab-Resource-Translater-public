using Ellab_Resource_Translater.Enums;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ellab_Resource_Translater.Util
{
    public class DBStringHandler
    {
        /// <summary>
        /// Tries to detect which database it is and return a relevant DBConnection.
        /// </summary>
        /// <param name="connectionString">string or JsonString</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static DbConnection CreateDbConnection(string connectionString)
        {
            DbConnection output = DetectType(connectionString) switch {
                ConnType.MySql or ConnType.MSSqlIS => new MySqlConnection(connectionString),
                ConnType.MSSql => new SqlConnection(connectionString),
                ConnType.PostgreSql => new NpgsqlConnection(connectionString),
                _ => throw new InvalidOperationException("Unknown or unsupported database type in connection string.")
            };
            output.ConnectionString = output.ConnectionString.Replace("MultipleActiveResultSets=False","MultipleActiveResultSets=True");
            return output;
        }

        /// <summary>
        /// Tries to detect which database it is.
        /// </summary>
        /// <param name="connectionString">string or JsonString</param>
        /// <returns>Invalid: ConnType.None</returns>
        public static ConnType DetectType(string connectionString)
        {
            // In Case it's a Json Object gotten from exporting connection from VS
            connectionString = JsonExtractIfNeeded(connectionString);

            // Each Connection type have different setups
            if ((connectionString.Contains("Server")
                || connectionString.Contains("Data Source"))
                && connectionString.Contains("User ID")
                && connectionString.Contains("Password"))
                return ConnType.MSSql;

            else if ((connectionString.Contains("Server")
                     || connectionString.Contains("Data Source"))
                     && connectionString.Contains("Integrated Security=True"))
                return ConnType.MSSqlIS;

            else if ((connectionString.Contains("Data Source")
                     || connectionString.Contains("Server"))
                     && connectionString.Contains("Uid")
                     && connectionString.Contains("Pwd"))
                return ConnType.MySql;

            else if ((connectionString.Contains("Data Source")
                     || connectionString.Contains("Server"))
                     && connectionString.Contains("IntegratedSecurity = yes")
                     && (connectionString.Contains("Uid=auth_windows"))
                     || connectionString.Contains("User ID=auth_windows"))
                return ConnType.MySqlIS;

            else if (connectionString.Contains("Host")
                     && connectionString.Contains("Username")
                     && connectionString.Contains("Password"))
                return ConnType.PostgreSql;

            else
                return ConnType.None;
        }

        /// <summary>
        /// Tries to Extract connection string from JsonString otherwise returns input (supposedly the connection string)
        /// </summary>
        /// <param name="connectionString">string or JsonString</param>
        /// <returns></returns>
        public static string JsonExtractIfNeeded(string connectionString)
        {
            JsonSerializerSettings dontThrow = new()
            {
                Error = (s,a) => { a.ErrorContext.Handled = true; }
            };
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(connectionString, dontThrow);
            if (dict != null && dict.Count > 0)
                return dict.First().Value;

            dict = JsonConvert.DeserializeObject<Dictionary<string, string>>($"{{ {connectionString} }}", dontThrow);
            if (dict != null && dict.Count > 0)
                connectionString = dict.First().Value;

            return connectionString;
        }
    }
}
