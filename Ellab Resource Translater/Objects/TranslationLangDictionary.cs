using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public sealed class TranslationLangDictionary<T>(Dictionary<string, Dictionary<string, MetaData<T>>> dict)
    {
        public Dictionary<string, Dictionary<string, MetaData<T>>> Dict => dict;
    }
}
