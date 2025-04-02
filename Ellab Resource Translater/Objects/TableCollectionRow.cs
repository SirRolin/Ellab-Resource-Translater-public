using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public sealed class TableCollectionRow(int dataTNum, DataRow row)
    {
        public int DataTNum { get; } = dataTNum;
        public DataRow Row { get; } = row;
    }
}
