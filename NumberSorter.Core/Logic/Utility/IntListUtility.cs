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

        public static int FindMinimum(IList<int> list, int startingIndex, int length)
        {
            int minimum = int.MaxValue;
            int indexLimit = startingIndex + length;
            for (int i = 0; i != indexLimit; i++)
                minimum = Math.Min(minimum, list[i]);
            return minimum;
        }

        public static int FindMaximum(IList<int> list, int startingIndex, int length)
        {
            int maximum = int.MinValue;
            int indexLimit = startingIndex + length;
            for (int i = 0; i != indexLimit; i++)
                maximum = Math.Max(maximum, list[i]);
            return maximum;
        }

        public static int FindMaxLog(IList<int> list, int startingIndex, int length, int baseValue)
        {
            int maximum = FindMaximum(list, startingIndex, length);
            return (int)(Math.Log(maximum) / Math.Log(baseValue));
        }

        public static void AddValue(IList<int> list, int startingIndex, int length, int value)
        {
            int indexLimit = startingIndex + length;
            for (int i = 0; i != indexLimit; i++)
                list[i] += value;
        }

        public static void InvertPartAndNumbers(IList<int> list, int startingIndex, int length)
        {
            int leftIndex = startingIndex;
            int rightIndex = startingIndex + length - 1;
            int swapCount = length / 2;

            int indexLimit = startingIndex + swapCount;
            while (leftIndex != indexLimit)
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
