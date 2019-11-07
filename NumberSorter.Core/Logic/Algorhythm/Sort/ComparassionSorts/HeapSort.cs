using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class HeapSort<T> : GenericSortAlgorhythm<T>
    {
        public HeapSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            var heapSize = list.Count;
            for (int i = heapSize / 2 - 1; i >= 0; i--)
                MaxHeapify(list, heapSize, i);

            for (int i = heapSize - 1; i >= 0; i--)
            {
                list.Swap(0, i);
                MaxHeapify(list, i, 0);
            }
        }

        private void BuildHeap(IList<T> list, int heapSize)
        {
            int startingIndex = heapSize / 2;
            for (int i = startingIndex; i >= 0; i--)
                MaxHeapify(list, heapSize, i);
        }

        private void MaxHeapify(IList<T> list, int heapSize, int parenIndex)
        {
            int largestIndex = parenIndex;
            int leftIndex = (2 * parenIndex) + 1;
            int rightIndex = (2 * parenIndex) + 2;

            if (leftIndex < heapSize && Compare(list, leftIndex, largestIndex) > 0)
                largestIndex = leftIndex;
            if (rightIndex < heapSize && Compare(list, rightIndex, largestIndex) > 0)
                largestIndex = rightIndex;

            if (largestIndex != parenIndex)
            {
                list.Swap(largestIndex, parenIndex);
                MaxHeapify(list, heapSize, largestIndex);
            }
        }
    }
}
