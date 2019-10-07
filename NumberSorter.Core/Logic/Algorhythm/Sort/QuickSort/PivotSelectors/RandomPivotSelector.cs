using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.QuickSort.PivotSelectors
{
    public sealed class RandomPivotSelector<T> : QuickSortPivotSelector<T>
    {
        private readonly Random _random = new Random();

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex, IComparer<T> comparer)
        {
            return _random.Next(firstIndex, lastIndex);
        }
    }
}
