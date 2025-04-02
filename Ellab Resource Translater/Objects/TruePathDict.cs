using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public sealed class TruePathDict(IEnumerable<string> paths)
    {
        public Dictionary<string, string> Dict => paths.Select(static truePath => new KeyValuePair<string, string>(truePath.ToLowerInvariant(), truePath))
                                                                                     .ToDictionary();
    }
}
