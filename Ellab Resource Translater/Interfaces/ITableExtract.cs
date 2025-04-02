using Ellab_Resource_Translater.Objects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Interfaces
{
    public interface ITableExtract<T> where T : ITableExtract<T>
    {
        public abstract static bool TryExtract(Indexed<DataTable> dt, Action myUpdate, ConcurrentQueue<TableCollectionRow> dataRows, [MaybeNullWhen(false)] out T ctc);
    }
}
