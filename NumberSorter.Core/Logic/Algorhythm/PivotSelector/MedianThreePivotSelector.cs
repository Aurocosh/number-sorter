using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    public sealed class MedianThreePivotSelector<T> : GenericPivotSelector<T>
    {
        public MedianThreePivotSelector(IComparer<T> comparer) : base(comparer) { }

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex)
        {
            int centerIndex = (firstIndex + lastIndex) / 2;

            if (Compare(list[centerIndex], list[firstIndex]) < 0)
                list.Swap(firstIndex, centerIndex);
            if (Compare(list[lastIndex], list[firstIndex]) < 0)
                list.Swap(firstIndex, lastIndex);
            if (Compare(list[lastIndex], list[centerIndex]) < 0)
                list.Swap(centerIndex, lastIndex);
            return centerIndex;
        }
    }
}
