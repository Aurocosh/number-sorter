using NumberSorter.Domain.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    public class GallopingRecursiveMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public GallopingRecursiveMergeSort(IComparer<T> comparer) : base(comparer) { }

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

            int firstIndex = 0;
            int secondIndex = 0;
            int mergedIndex = 0;

            int firstReapeats = 0;
            int secondReapeats = 0;

            while (firstIndex != firstLength && secondIndex != secondLength)
            {
                var nextFromFirst = firstArray[firstIndex];
                var nextFromSecond = secondArray[secondIndex];

                int comparassion = Compare(nextFromFirst, nextFromSecond);
                if (comparassion > 0)
                {
                    if (secondReapeats > 7)
                    {
                        int gallopSize = Gallop(secondIndex, 8, firstArray[firstIndex], secondArray);
                        Array.Copy(secondArray, secondIndex, mergedArray, mergedIndex, gallopSize);
                        secondReapeats = 0;
                        secondIndex += gallopSize;
                        mergedIndex += gallopSize;
                    }
                    else
                    {
                        secondIndex++;
                        secondReapeats++;
                        firstReapeats = 0;
                        mergedArray[mergedIndex++] = nextFromSecond;
                    }
                }
                else
                {
                    if (firstReapeats > 7)
                    {
                        int gallopSize = Gallop(firstIndex, 8, secondArray[secondIndex], firstArray);
                        Array.Copy(firstArray, firstIndex, mergedArray, mergedIndex, gallopSize);
                        firstReapeats = 0;
                        firstIndex += gallopSize;
                        mergedIndex += gallopSize;
                    }
                    else
                    {
                        firstIndex++;
                        firstReapeats++;
                        secondReapeats = 0;
                        mergedArray[mergedIndex++] = nextFromFirst;
                    }
                }
            }

            if (firstIndex != firstLength)
                Array.Copy(firstArray, firstIndex, mergedArray, mergedIndex, firstLength - firstIndex);
            else
                Array.Copy(secondArray, secondIndex, mergedArray, mergedIndex, secondLength - secondIndex);

            return mergedArray;
        }

        private int Gallop(int start, int step, T otherValue, T[] array)
        {
            int index = start;
            int galllopSize = 1;
            int size = array.Length;

            if (Compare(array[size - 1], otherValue) <= 0)
                return size - start;

            index += step;
            while (index < size && Compare(array[index], otherValue) <= 0)
            {
                galllopSize += step;
                step *= 2;
                index += step;
            }

            return galllopSize;
        }
    }
}
