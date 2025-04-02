using Ellab_Resource_Translater.Objects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Interfaces
{
    public interface IRegisterInGroup<T, C>
    {
        public abstract static void Register(IDictionary<string, List<T>> Dict, TableCollectionRow tcr, ConcurrentDictionary<int, C> ctcs);
    }
}
