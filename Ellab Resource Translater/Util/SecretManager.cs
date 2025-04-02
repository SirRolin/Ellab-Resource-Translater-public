using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Ellab_Resource_Translater.Util
{
    public class SecretManager
    {
        public static void SetUserSecret(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.User);
        }

        public static string? GetUserSecret(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
        }

        public static void DeleteUserSecret(string key)
        {
            Environment.SetEnvironmentVariable(key, null, EnvironmentVariableTarget.User);
        }
    }

}
