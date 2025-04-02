using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Ellab_Resource_Translater.Objects
{
    public class MetaData<Type>(string key, Type value, string comment, string language = "")
    {
        public string key = key;
        public Type value = value;
        public string comment = comment;
        public string language = language;

        override
        public string ToString()
        {
            return string.Concat(language, "|", key, ": ", value?.ToString() ?? "null", " - ", comment);
        }
    }
}
