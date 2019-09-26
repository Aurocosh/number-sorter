using NumberSorter.Algorhythm;
using NumberSorter.Logic.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    public class QuickSort<T> : ComparerSort<T>
    {
        private readonly Random _random = new Random();

        public QuickSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            SortRange(list, 0, list.Count - 1);
        }

        private void SortRange(IList<T> list, int firstIndex, int lastIndex)
        {
            if (firstIndex >= lastIndex)
                return;

            int pivotIndex = SelectPivot(firstIndex, lastIndex);
            var pivot = list[pivotIndex];

            list.Swap(firstIndex, pivotIndex);
            pivotIndex = firstIndex;
            int nextBigElementIndex = lastIndex;
            int nextUnsortedIndex = pivotIndex + 1;
            int unsortedElementCount = lastIndex - firstIndex;
            while (unsortedElementCount-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                var comparrassion = Compare(pivot, nextUnsorted);
                if (comparrassion >= 0)
                    list.Swap(pivotIndex++, nextUnsortedIndex++);
                else
                    list.Swap(nextUnsortedIndex, nextBigElementIndex--);
            }
            SortRange(list, firstIndex, pivotIndex - 1);
            SortRange(list, pivotIndex + 1, lastIndex);
        }

        private int SelectPivot(int firstIndex, int lastIndex)
        {
            return _random.Next(firstIndex, lastIndex);
        }
    }
}
