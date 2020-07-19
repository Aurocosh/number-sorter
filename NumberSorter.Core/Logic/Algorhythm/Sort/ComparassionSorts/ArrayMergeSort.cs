using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class ArrayMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public ArrayMergeSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var array = list.GetRangeAsArray(startingIndex, length);
            var sortedArray = MergeSort(array);
            ListUtility.Copy(sortedArray, 0, list, startingIndex, length);
        }

        private T[] MergeSort(T[] array)
        {
            if (array.Length == 1)
                return array;

            var halvesOfArray = ArrayUtility.SplitArray(array);
            var firstSorted = MergeSort(halvesOfArray.First);
            var secondSorted = MergeSort(halvesOfArray.Second);

            return Merge(firstSorted, secondSorted);

        }

        private T[] Merge(T[] firstArray, T[] secondArray)
        {
            int mergedCount = firstArray.Length + secondArray.Length;
            var mergedArray = new T[mergedCount];

            int firstLength = firstArray.Length;
            int secondLength = secondArray.Length;

            int firstIndex = 0;
            int secondIndex = 0;
            int mergedIndex = 0;

            while (firstIndex != firstLength && secondIndex != secondLength)
            {
                var nextFromFirst = firstArray[firstIndex];
                var nextFromSecond = secondArray[secondIndex];

                int comparassion = Compare(nextFromFirst, nextFromSecond);
                if (comparassion > 0)
                {
                    secondIndex++;
                    mergedArray[mergedIndex++] = nextFromSecond;
                }
                else
                {
                    firstIndex++;
                    mergedArray[mergedIndex++] = nextFromFirst;
                }
            }

            if (firstIndex != firstLength)
                Array.Copy(firstArray, firstIndex, mergedArray, mergedIndex, firstLength - firstIndex);
            else
                Array.Copy(secondArray, secondIndex, mergedArray, mergedIndex, secondLength - secondIndex);

            return mergedArray;
        }
    }
}
