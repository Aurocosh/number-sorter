using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

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

            int lastIndex = indexLimit - 1;
            int leftIndex = startingIndex;
            int rightIndex = lastIndex;

            while (leftIndex <= rightIndex)
            {
                while (leftIndex < lastIndex && list[leftIndex] < average)
                    leftIndex++;
                while (rightIndex >= startingIndex && list[rightIndex] >= average)
                    rightIndex--;
                if (leftIndex <= rightIndex)
                    list.Swap(leftIndex++, rightIndex--);
            }

            int firstLength = leftIndex - startingIndex;
            if (firstLength == length || firstLength == 0)
                return;

            int secondLength = length - firstLength;
            int secondIndex = startingIndex + firstLength;

            Sort(list, startingIndex, firstLength);
            Sort(list, secondIndex, secondLength);
        }
    }
}
