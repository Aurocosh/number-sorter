using NumberSorter.Logic.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm.QuickSort.PivotSelectors
{
    internal sealed class MedianThreePivotSelector<T> : QuickSortPivotSelector<T>
    {
        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex, IComparer<T> comparer)
        {
            int centerIndex = (firstIndex + lastIndex) / 2;

            if (comparer.Compare(list[centerIndex], list[firstIndex]) < 0)
                list.Swap(firstIndex, centerIndex);
            if (comparer.Compare(list[lastIndex], list[firstIndex]) < 0)
                list.Swap(firstIndex, lastIndex);
            if (comparer.Compare(list[lastIndex], list[centerIndex]) < 0)
                list.Swap(centerIndex, lastIndex);
            return centerIndex;
        }
    }
}
