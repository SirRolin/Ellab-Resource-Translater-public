using Ellab_Resource_Translater.Interfaces;
using Ellab_Resource_Translater.Util;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public sealed class GroupChanges<T, C> where C : IRegisterInGroup<T, C>, ITableExtract<C>
    {
        public readonly ConcurrentDictionary<string, List<T>> Dict = [];
        public GroupChanges(int maxThreads,
                            ListView view,
                            ConcurrentTable<C> cTable,
                            Action<string, Ref<int>, int> myUpdate,
                            string title)
        {
            Ref<int> currentProgress = 0;
            int rowCount = cTable.dataRows.Count;

            // Initial Update of UI
            myUpdate(title, currentProgress, rowCount);
            ExecutionHandler.Execute(maxThreads, rowCount, (threadNum) =>
                {
                    while (cTable.dataRows.TryDequeue(out var rowData))
                    {
                        FormUtils.ShowOnListWhileProcessing(
                            onStart: () => myUpdate(title, currentProgress, rowCount),
                            listView: view,
                            processName: threadNum + ") Fetching Data...",
                            process: () => C.Register(Dict, rowData, cTable.dataColumns));
                    }
                });
        }
    }
}
