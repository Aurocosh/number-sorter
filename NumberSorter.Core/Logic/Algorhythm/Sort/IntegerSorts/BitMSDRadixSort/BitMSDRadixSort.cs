using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class BitMSDRadixSort : IIntegerSortAlgorhythm
    {
        public void Sort(IList<int> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<int> list, int startingIndex, int length)
        {
            Sort(list, startingIndex, length, 0);
        }

        public void Sort(IList<int> list, int startingIndex, int length, int shift)
        {
            if (length < 2)
                return;
            if (shift > 31)
                return;

            int lastIndex = startingIndex + length - 1;
            int leftIndex = startingIndex;
            int rightIndex = lastIndex;

            while (leftIndex <= rightIndex)
            {
                while (leftIndex < lastIndex && (shift == 0 ? (list[leftIndex] << shift) < 0 : (list[leftIndex] << shift) >= 0))
                    leftIndex++;
                while (rightIndex >= startingIndex && (shift == 0 ? (list[rightIndex] << shift) >= 0 : (list[rightIndex] << shift) < 0))
                    rightIndex--;
                if (leftIndex <= rightIndex)
                    list.Swap(leftIndex++, rightIndex--);
            }

            int leftLength = leftIndex - startingIndex;
            int rightLength = length - leftLength;

            Sort(list, startingIndex, leftLength, shift + 1);
            Sort(list, startingIndex + leftLength, rightLength, shift + 1);
        }
    }
}
