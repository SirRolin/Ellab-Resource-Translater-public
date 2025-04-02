using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Structs
{
    internal readonly struct CheckResult(bool check, string value)
    {
        public readonly bool Check => check;
        public readonly string Value => value;
    }
}
