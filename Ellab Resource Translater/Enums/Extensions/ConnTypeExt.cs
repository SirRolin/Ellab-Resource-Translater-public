using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Enums.Extensions
{
    public static class ConnTypeExtender
    {
        public static bool HasAny(this ConnType tct, ConnType ct)
        {
            return (ct & tct) != 0;
        }
        public static bool HasAll(this ConnType tct, ConnType ct)
        {
            return (ct & tct) == tct;
        }
    }
}
