using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects.Extensions
{
    static class MetaDataExt
    {
        public static ResXDataNode? ToResXDataNode(this MetaData<object?> meta)
        {
            if (meta.value is ISerializable iSer)
                return new(meta.key, iSer)
                {
                    Comment = meta.comment
                };
            return null;
        }

        public static void WriteToResourceWriter(this MetaData<object?> meta, ResXResourceWriter writer)
        {
            if (meta.value is ISerializable iSer)
            {
                writer.AddResource(new(meta.key, iSer)
                {
                    Comment = meta.comment
                });
            } 
            // Cause Apparently strings are not ISerializable. Though they can be serialized.
            else if (meta.value is string iStr && iStr != string.Empty)
            {
                writer.AddResource(new(meta.key, iStr)
                {
                    Comment = meta.comment
                });
            }
        }

        public static Dictionary<string, MetaData<Type>> FilterTo<Type>(this Dictionary<string, MetaData<object?>> metaData)
        {
            Dictionary<string, MetaData<Type>> output = [];
            foreach (var item in metaData)
            {
                if (item.Value.value is Type typed)
                {
                    output.Add(item.Key, new MetaData<Type>(item.Key, typed, item.Value.comment, item.Value.language));
                }
            }
            return output;
        }

        public static Dictionary<string, MetaData<string>> FilterKeyStartsOut(this IEnumerable<KeyValuePair<string, MetaData<string>>> metaData, params string[] strings)
        {
            IEnumerable<KeyValuePair<string, MetaData<string>>> output = metaData;
            foreach (var filter in strings)
            {
                output = output.Where(x => !x.Key.StartsWith(filter));
            }
            return output.ToDictionary();
        }

        public static List<MetaData<string>> FilterKeyStartsOut(this IEnumerable<MetaData<string>> metaData, params string[] strings)
        {
            IEnumerable<MetaData<string>> output = metaData;
            foreach (var filter in strings)
            {
                output = output.Where(x => !x.key.StartsWith(filter));
            }
            return [.. output];
        }
    }
}
