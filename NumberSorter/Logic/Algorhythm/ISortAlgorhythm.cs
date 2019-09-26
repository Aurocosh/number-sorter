using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm
{
    public interface ISortAlgorhythm<T>
    {
        int Compare(T first, T second);
        void Sort(IList<T> list);
    }
}
