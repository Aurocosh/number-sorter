using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class OpRecursiveMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public OpRecursiveMergeSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            var array = list.ToArray();
            var sortedArray = MergeSort(array);

            var length = list.Count;
            for (int i = 0; i < length; i++)
                list[i] = sortedArray[i];
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

            int firstLimit = firstArray.Length - 1;
            int secondLimit = secondArray.Length - 1;

            int firstIndex = 0;
            int secondIndex = 0;
            int mergedIndex = 0;

            var nextFromFirst = firstArray[firstIndex];
            var nextFromSecond = secondArray[secondIndex];

            while (firstIndex != firstLimit && secondIndex != secondLimit)
            {
                int comparassion = Compare(nextFromFirst, nextFromSecond);
                if (comparassion > 0)
                {
                    secondIndex++;
                    mergedArray[mergedIndex++] = nextFromSecond;
                    nextFromSecond = secondArray[secondIndex];
                }
                else
                {
                    firstIndex++;
                    mergedArray[mergedIndex++] = nextFromFirst;
                    nextFromFirst = firstArray[firstIndex];
                }
            }

            while (firstIndex != firstLength && secondIndex != secondLength)
            {
                nextFromFirst = firstArray[firstIndex];
                nextFromSecond = secondArray[secondIndex];

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
