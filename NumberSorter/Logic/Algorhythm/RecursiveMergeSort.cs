using NumberSorter.Algorhythm;
using NumberSorter.Logic.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    public class RecursiveMergeSort : ISortAlgorhythm
    {
        private sealed class ArrayHalves<T>
        {
            public T[] First { get; }
            public T[] Second { get; }

            public ArrayHalves(T[] first, T[] second)
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

        public void Sort<T>(IList<T> list, IComparer<T> comparer)
        {
            var array = list.ToArray();
            var sortedArray = MergeSort(array, comparer);

            var length = list.Count;
            for (int i = 0; i < length; i++)
                list[i] = sortedArray[i];
        }

        private static T[] MergeSort<T>(T[] array, IComparer<T> comparer)
        {
            if (array.Length == 1)
                return array;

            var halvesOfArray = SplitArray(array);
            var firstSorted = MergeSort(halvesOfArray.First, comparer);
            var secondSorted = MergeSort(halvesOfArray.Second, comparer);

            return Merge(firstSorted, secondSorted, comparer);

        }

        private static T[] Merge<T>(T[] firstArray, T[] secondArray, IComparer<T> comparer)
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

                    int comparassion = comparer.Compare(nextFromFirst, nextFromSecond);
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

        private static ArrayHalves<T> SplitArray<T>(T[] array)
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
    }
}
