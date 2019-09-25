using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm
{
    public interface ISortAlgorhythm
    {
        void Sort<T>(IList<T> list, IComparer<T> comparer);
    }
}
