using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm.QuickSort
{
    abstract public class QuickSortPivotSelector<T>
    {
        abstract public int SelectPivot(IList<T> list, int firstIndex, int lastIndex, IComparer<T> comparer);
    }
}
