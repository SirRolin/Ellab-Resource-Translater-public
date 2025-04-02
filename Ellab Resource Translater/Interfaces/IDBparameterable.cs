using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Interfaces
{
    public interface IDBparameterable
    {
        DbParameter CreateParameter();
        DbParameterCollection Parameters { get; }
    }
}
