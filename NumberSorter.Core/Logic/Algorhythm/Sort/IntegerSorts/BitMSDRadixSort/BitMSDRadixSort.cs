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

            int nextBigIndex = startingIndex + length - 1;
            int nextUnsortedIndex = startingIndex;
            int unsortedElementCount = length;
            while (unsortedElementCount-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                bool move = (nextUnsorted << shift) >= 0;
                if (shift == 0 ? !move : move)
                    nextUnsortedIndex++;
                else
                    list.Swap(nextUnsortedIndex, nextBigIndex--);
            }
            int leftLength = nextUnsortedIndex - startingIndex;
            int rightLength = length - leftLength;

            Sort(list, startingIndex, leftLength, shift + 1);
            Sort(list, startingIndex + leftLength, rightLength, shift + 1);
        }
    }
}
