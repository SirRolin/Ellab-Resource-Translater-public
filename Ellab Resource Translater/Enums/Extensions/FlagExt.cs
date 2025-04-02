using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Enums.Extensions
{
    public static class FlagExt
    {
        public static bool HasAny<T>(this T tct, T ct) where T : Enum
        {
            return (Convert.ToInt32(ct) & Convert.ToInt32(tct)) != 0;
        }
        public static bool HasAll<T>(this T tct, T ct) where T : Enum
        {
            return (Convert.ToInt32(ct) & Convert.ToInt32(tct)) == Convert.ToInt32(tct);
        }
    }
}
