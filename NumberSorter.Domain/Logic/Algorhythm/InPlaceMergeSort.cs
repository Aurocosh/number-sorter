using NumberSorter.Domain.Logic.Container;
using NumberSorter.Domain.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    public class InPlaceMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public InPlaceMergeSort(IComparer<T> comparer) : base(comparer)
        {
        }

        private sealed class ArrayHalves<K>
        {
            public SortRun First { get; }
            public SortRun Second { get; }

            public ArrayHalves(SortRun first, SortRun second)
            {
                First = first;
                Second = second;
            }
        }

        public override void Sort(IList<T> list)
        {
            var sortRun = new SortRun(0, list.Count);
            MergeSort(list, sortRun);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            var halvesOfSortRun = SplitSortRun(sortRun);
            MergeSort(list, halvesOfSortRun.First);
            MergeSort(list, halvesOfSortRun.Second);

            Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }

        private static ArrayHalves<T> SplitSortRun(SortRun sortRun)
        {
            if (sortRun.Length == 0)
                return new ArrayHalves<T>(sortRun, sortRun);
            if (sortRun.Length == 1)
                return new ArrayHalves<T>(sortRun, new SortRun(sortRun.Start, 0));


            int firstIndex = sortRun.Start;
            int firstLength = sortRun.Length / 2;

            int secondIndex = sortRun.Start + firstLength;
            int secondLength = sortRun.Length - firstLength;

            var firstSortRun = new SortRun(firstIndex, firstLength);
            var secondSortRun = new SortRun(secondIndex, secondLength);
            return new ArrayHalves<T>(firstSortRun, secondSortRun);
        }

        private void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            var lastFirstIndex = firstRun.Start + secondRun.Length - 1;
            var lastSecondIndex = secondRun.Start + secondRun.Length - 1;

            var first = SortRunUtility.RunToString(list, firstRun);
            var second = SortRunUtility.RunToString(list, secondRun);

            Console.WriteLine($"\n\nFirst ({firstRun.Start},{firstRun.Length}) Second ({secondRun.Start},{secondRun.Length})");
            Console.WriteLine($"\nBefore {first}   {second}");

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int tempLength = 0;
            int tempStartIndex = secondRun.Start;
            int tempCurrentIndex = secondRun.Start;

            while (firstIndex != secondIndex && tempStartIndex <= lastSecondIndex)
            {
                bool isUsingTemp = tempLength > 0;
                bool hasSecond = secondIndex <= lastSecondIndex;
                var nextFromFirst = isUsingTemp ? list[tempCurrentIndex] : list[firstIndex];

                if (hasSecond && Compare(nextFromFirst, list[secondIndex]) > 0)
                {
                    list.Swap(firstIndex, secondIndex);
                    secondIndex++;
                    tempLength++;
                }
                else if (isUsingTemp)
                {
                    list.Swap(firstIndex, tempCurrentIndex);
                    tempCurrentIndex++;
                    tempCurrentIndex = WrapOverflow(tempCurrentIndex, tempStartIndex, tempStartIndex + tempLength - 1);
                }

                firstIndex++;
                if (firstIndex == tempStartIndex && tempLength > 0)
                {
                    int limit = firstIndex + tempLength;
                    for (int i = firstIndex; i < limit; i++)
                    {
                        if (i != tempCurrentIndex)
                            list.Swap(i, tempCurrentIndex);
                        tempLength--;
                        tempStartIndex++;
                        tempCurrentIndex++;
                        tempCurrentIndex = WrapOverflow(tempCurrentIndex, tempStartIndex, tempStartIndex + tempLength - 1);
                    }
                }

                first = SortRunUtility.RunToString(list, firstRun);
                second = SortRunUtility.RunToString(list, secondRun);
                Console.WriteLine($"\n{firstIndex} After {first}   {second}");
            }


            first = SortRunUtility.RunToString(list, firstRun);
            second = SortRunUtility.RunToString(list, secondRun);
            Console.WriteLine($"\nAfter {first}   {second}");
        }

        private int WrapOverflow(int index, int start, int max)
        {
            if (index < start || index > max)
                return start;
            return index;
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
