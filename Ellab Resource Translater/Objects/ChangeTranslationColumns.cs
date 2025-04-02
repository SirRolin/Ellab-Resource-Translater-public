using Ellab_Resource_Translater.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public sealed class ChangeTranslationColumns(DataColumn resource, DataColumn key, DataColumn value, DataColumn comment, DataColumn language) : ITableExtract<ChangeTranslationColumns>, IRegisterInGroup<MetaData<object?>, ChangeTranslationColumns>
    {
        public DataColumn Resource { get; } = resource;
        public DataColumn Key { get; } = key;
        public DataColumn Value { get; } = value;
        public DataColumn Comment { get; } = comment;
        public DataColumn Language { get; } = language;

        public static void Register(ConcurrentDictionary<string, List<MetaData<object?>>> Dict, TableCollectionRow tcr, ConcurrentDictionary<int, ChangeTranslationColumns> ctcs, Func<string, string> langToLocal)
        {
            if (tcr.Row[ctcs[tcr.DataTNum].Resource] is string resourceValue
                && tcr.Row[ctcs[tcr.DataTNum].Key] is string keyValue
                && tcr.Row[ctcs[tcr.DataTNum].Value] is string valueValue
                && tcr.Row[ctcs[tcr.DataTNum].Comment] is string commentValue
                && tcr.Row[ctcs[tcr.DataTNum].Language] is string languageValue)
            {
                if (!languageValue.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    resourceValue = resourceValue.Insert(resourceValue.Length - 5, langToLocal(languageValue));
                }
                Dict.AddOrUpdate(key: resourceValue,
                    addValue: [new MetaData<object?>(keyValue, valueValue, commentValue, languageValue)],
                    updateValueFactory: (key, orgList) =>
                    {
                        orgList.Add(new MetaData<object?>(keyValue, valueValue, commentValue, languageValue));
                        return orgList;
                    });
            }
        }


        public static bool TryExtract(Indexed<DataTable> dt, Action myUpdate, ConcurrentQueue<TableCollectionRow> dataRows, [MaybeNullWhen(false)] out ChangeTranslationColumns ctc)
        {
            if (dt.Item.Columns["ResourceName"] is DataColumn resourceColumn
                && dt.Item.Columns["Key"] is DataColumn keyColumn
                && dt.Item.Columns["ChangedText"] is DataColumn textColumn
                && dt.Item.Columns["Comment"] is DataColumn commentValue
                && dt.Item.Columns["LanguageCode"] is DataColumn languageValue)
            {
                ctc = new(resourceColumn, keyColumn, textColumn, commentValue, languageValue);
                foreach (DataRow row in dt.Item.Rows)
                {
                    dataRows.Enqueue(new TableCollectionRow(dt.Index, row));
                    myUpdate();
                }
                return true;
            }
            ctc = null;
            return false;
        }
    }
}
