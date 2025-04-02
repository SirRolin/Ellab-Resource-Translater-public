using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Objects
{
    public class Indexed<T>(T item, int index)
    {
        public int Index => index;
        public T Item => item;
    }
}
