using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    public class RecursiveMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public RecursiveMergeSort(IComparer<T> comparer) : base(comparer) { }

        private sealed class ArrayHalves<K>
        {
            public K[] First { get; }
            public K[] Second { get; }

            public ArrayHalves(K[] first, K[] second)
            {
                First = first;
                Second = second;
            }
        }

        private enum ArrayLeftover
        {
            First,
            Second,
            Both
        }

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

            var halvesOfArray = SplitArray(array);
            var firstSorted = MergeSort(halvesOfArray.First);
            var secondSorted = MergeSort(halvesOfArray.Second);

            return Merge(firstSorted, secondSorted);

        }

        private static ArrayHalves<T> SplitArray(T[] array)
        {
            if (array.Length == 0)
                return new ArrayHalves<T>(new T[0], new T[0]);
            if (array.Length == 1)
                return new ArrayHalves<T>(array.ToArray(), new T[0]);

            const int firstIndex = 0;
            int secondIndex = array.Length / 2;

            int firstLength = secondIndex;
            int secondLength = array.Length - firstLength;

            var firstArray = new T[firstLength];
            var secondArray = new T[secondLength];

            Array.Copy(array, firstIndex, firstArray, 0, firstLength);
            Array.Copy(array, secondIndex, secondArray, 0, secondLength);

            return new ArrayHalves<T>(firstArray, secondArray);
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
            ArrayLeftover arrayLeftover = ArrayLeftover.Both;

            while (mergedIndex < mergedCount && arrayLeftover == ArrayLeftover.Both)
            {
                if (firstIndex == firstLength)
                {
                    arrayLeftover = ArrayLeftover.Second;
                }
                else if (secondIndex == secondLength)
                {
                    arrayLeftover = ArrayLeftover.First;
                }
                else
                {
                    var nextFromFirst = firstArray[firstIndex];
                    var nextFromSecond = secondArray[secondIndex];

                    int comparassion = Compare(nextFromFirst, nextFromSecond);
                    if (comparassion > 0)
                    {
                        secondIndex++;
                        mergedArray[mergedIndex] = nextFromSecond;
                    }
                    else
                    {
                        firstIndex++;
                        mergedArray[mergedIndex] = nextFromFirst;
                    }

                    mergedIndex++;
                }
            }

            if (arrayLeftover == ArrayLeftover.First)
                Array.Copy(firstArray, firstIndex, mergedArray, mergedIndex, firstLength - firstIndex);
            else if (arrayLeftover == ArrayLeftover.Second)
                Array.Copy(secondArray, secondIndex, mergedArray, mergedIndex, secondLength - secondIndex);

            return mergedArray;
        }

    }
}
