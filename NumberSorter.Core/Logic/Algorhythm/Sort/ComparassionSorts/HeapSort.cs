using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class HeapSort<T> : GenericSortAlgorhythm<T>
    {
        public HeapSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;
            for (int i = startingIndex + (length / 2 - 1); i >= startingIndex; i--)
                MaxHeapify(list, indexLimit, i);

            for (int i = indexLimit - 1; i >= startingIndex; i--)
            {
                list.Swap(startingIndex, i);
                MaxHeapify(list, i, startingIndex);
            }
        }

        private void MaxHeapify(IList<T> list, int indexLimit, int parenIndex)
        {
            int largestIndex = parenIndex;
            int leftIndex = (2 * parenIndex) + 1;
            int rightIndex = (2 * parenIndex) + 2;

            if (leftIndex < indexLimit && Compare(list, leftIndex, largestIndex) > 0)
                largestIndex = leftIndex;
            if (rightIndex < indexLimit && Compare(list, rightIndex, largestIndex) > 0)
                largestIndex = rightIndex;

            if (largestIndex != parenIndex)
            {
                list.Swap(largestIndex, parenIndex);
                MaxHeapify(list, indexLimit, largestIndex);
            }
        }
    }
}
