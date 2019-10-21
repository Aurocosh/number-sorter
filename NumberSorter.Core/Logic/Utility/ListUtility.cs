using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Utility
{
    public static class ListUtility
    {
        public static void Shuffle<T>(this IList<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Randomize(IList<int> list, int minimum, int maximum, Random random)
        {
            int size = list.Count;
            for (int i = 0; i < size; i++)
                list[i] = random.Next(minimum, maximum);
        }

        public static bool IsSorted<T>(IList<T> list, IComparer<T> comparer)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var first = list[i];
                var second = list[i + 1];

                int comparassion = comparer.Compare(first, second);
                if (comparassion > 0)
                    return false;
            }
            return true;
        }

        public static void InvertPart<T>(IList<T> list, int startingIndex, int length)
        {
            int leftIndex = startingIndex;
            int rightIndex = startingIndex + length - 1;
            int swapCount = length / 2;
            for (int i = 0; i < swapCount; i++)
                list.Swap(leftIndex++, rightIndex--);
        }

        public static bool IsSorted<T>(IReadOnlyList<T> list, IComparer<T> comparer)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var first = list[i];
                var second = list[i + 1];

                int comparassion = comparer.Compare(first, second);
                if (comparassion > 0)
                    return false;
            }
            return true;
        }

        public static IEnumerable<List<T>> SplitList<T>(IReadOnlyList<T> list, int chunkSize)
        {
            for (int i = 0; i < list.Count; i += chunkSize)
                yield return list.GetRange(i, Math.Min(chunkSize, list.Count - i));
        }

        public static List<T> GetRange<T>(this IReadOnlyList<T> list, int index, int count)
        {
            int upperLimit = index + count;
            var result = new List<T>(count);
            for (int i = index; i < upperLimit; i++)
                result.Add(list[i]);
            return result;
        }
    }
}
