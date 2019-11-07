using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class AverageBucketSort : IIntegerSortAlgorhythm
    {
        public void Sort(IList<int> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<int> list, int startingIndex, int length)
        {
            if (length < 2)
                return;

            double sum = 0;
            int indexLimit = startingIndex + length;
            for (int i = startingIndex; i != indexLimit; i++)
                sum += list[i];

            double average = sum / length;

            int nextBigElementIndex = indexLimit - 1;
            int nextUnsortedIndex = startingIndex;
            int unsortedElementCount = length;
            while (unsortedElementCount-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                if (nextUnsorted <= average)
                    nextUnsortedIndex++;
                else
                    list.Swap(nextUnsortedIndex, nextBigElementIndex--);
            }
            int firstLength = nextUnsortedIndex - startingIndex;
            if (firstLength == length)
                firstLength = length / 2;
            int secondLength = length - firstLength;
            int secondIndex = startingIndex + firstLength;

            Sort(list, startingIndex, firstLength);
            Sort(list, secondIndex, secondLength);
        }
    }
}
