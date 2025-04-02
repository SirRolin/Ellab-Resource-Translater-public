using Ellab_Resource_Translater.Interfaces;
using Ellab_Resource_Translater.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public class ConcurrentTable<T> where T : ITableExtract<T>
    {
        public readonly ConcurrentQueue<TableCollectionRow> dataRows = [];
        public readonly ConcurrentDictionary<int, T> dataColumns = [];

        public ConcurrentTable(
                int maxThreads,
                ListView view,
                IEnumerable<DataTable> dataTables,
                Action<string, Ref<int>, int> myUpdate)
        {
            Ref<int> tableProgress = -1;
            int tableCount = dataTables.Count();
            const string TITLE = "Merging tables: ";
            var indexedTables = new ConcurrentQueue<Indexed<DataTable>>([.. dataTables.Select((item, index) => new Indexed<DataTable>(item, index))]);

            // Initial Update of UI
            myUpdate(TITLE, tableProgress, tableCount);
            void processTable(Indexed<DataTable> idt)
            {
                if (T.TryExtract(idt, () => myUpdate(TITLE, tableProgress, tableCount), dataRows, out var ctc))
                {
                    dataColumns.TryAdd(idt.Index, ctc);
                }
            }
            ExecutionHandler.Execute(tableCount, maxThreads, (int threadNum) =>
            {
                while (indexedTables.TryDequeue(out var indexedTable))
                {
                    FormUtils.ShowOnListWhileProcessing(
                        onStart: () => myUpdate(TITLE, tableProgress, tableCount),
                        listView: view,
                        processName: threadNum + ") Fetching Data...",
                        process: () => processTable(indexedTable)
                        );
                }
            });
        }
    }
}
