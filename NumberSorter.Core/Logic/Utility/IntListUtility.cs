using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Utility
{
    public static class IntListUtility
    {
        public static void InvertNumbers(IList<int> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;
            for (int i = 0; i != indexLimit; i++)
                list[i] = -list[i];
        }

        public static void InvertPartAndNumbers(IList<int> list, int startingIndex, int length)
        {
            int leftIndex = startingIndex;
            int rightIndex = startingIndex + length - 1;
            int swapCount = length / 2;
            for (int i = 0; i < swapCount; i++)
            {
                int temp = -list[leftIndex];
                list[leftIndex] = -list[rightIndex];
                list[rightIndex] = temp;
                leftIndex++;
                rightIndex--;
            }
            if (swapCount * 2 < length)
            {
                int middleIndex = startingIndex + swapCount;
                list[middleIndex] = -list[middleIndex];
            }
        }
    }
}
