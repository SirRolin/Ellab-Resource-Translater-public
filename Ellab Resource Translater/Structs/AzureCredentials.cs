using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Structs
{
    internal readonly struct AzureCredentials(string key, string uRI, string region)
    {
        public readonly string Key => key;
        public readonly string URI => uRI;
        public readonly string Region => region;
    }
}
