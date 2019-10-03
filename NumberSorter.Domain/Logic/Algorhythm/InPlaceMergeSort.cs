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
            //var first = SortRunUtility.RunToString(list, firstRun);
            //var second = SortRunUtility.RunToString(list, secondRun);

            //Console.WriteLine($"\n\nFirst ({firstRun.Start},{firstRun.Length}) Second ({secondRun.Start},{secondRun.Length})");
            //Console.WriteLine($"\nStart {first}   {second}");

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int tempLength = 0;
            int tempStartIndex = secondRun.Start;
            int tempCurrentIndex = secondRun.Start;

            var lastSecondIndex = secondRun.Start + secondRun.Length - 1;
            while (firstIndex != secondIndex && tempStartIndex <= lastSecondIndex)
            {
                bool isUsingTemp = tempLength > 0;
                bool hasSecond = secondIndex <= lastSecondIndex;
                var nextFromFirst = isUsingTemp ? list[tempCurrentIndex] : list[firstIndex];
                //var nextFromSecond = hasSecond ? list[secondIndex].ToString() : "X";

                //first = SortRunUtility.RunToString(list, firstRun, firstIndex, secondIndex);
                //second = SortRunUtility.RunToString(list, secondRun, firstIndex, secondIndex);
                //var temp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
                //Console.WriteLine($"\n\nBefore");
                //Console.WriteLine($"First {nextFromFirst} g({firstIndex}) l({firstIndex - firstRun.Start})   Second {nextFromSecond} g({secondIndex}) l({secondIndex - secondRun.Start})");
                //Console.WriteLine($"{firstIndex} \n{first}\n{second}");
                //Console.WriteLine($"Temporary S({tempStartIndex}) C({tempCurrentIndex}) L({tempLength}) V({temp})");

                if (hasSecond && Compare(nextFromFirst, list[secondIndex]) > 0)
                {
                    list.Swap(firstIndex, secondIndex);
                    if (tempStartIndex != tempCurrentIndex)
                    {
                        //MergeSort(list, new SortRun(tempStartIndex, tempLength));
                        SortTemporary(list, tempStartIndex, tempLength, tempCurrentIndex);
                        tempCurrentIndex = tempStartIndex;
                    }

                    secondIndex++;
                    tempLength++;
                }
                else if (isUsingTemp)
                {
                    list.Swap(firstIndex, tempCurrentIndex);
                    tempCurrentIndex++;
                    tempCurrentIndex = WrapTempIndex(tempCurrentIndex, tempStartIndex, tempStartIndex + tempLength - 1);
                }

                firstIndex++;
                if (firstIndex == tempStartIndex && tempLength > 0)
                {
                    SortTemporary(list, tempStartIndex, tempLength, tempCurrentIndex);
                    tempStartIndex += tempLength;
                    tempCurrentIndex = tempStartIndex;
                    tempLength = 0;
                }

                //first = SortRunUtility.RunToString(list, firstRun, firstIndex, secondIndex);
                //second = SortRunUtility.RunToString(list, secondRun, firstIndex, secondIndex);
                //temp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
                //Console.WriteLine($"\nAfter");
                //Console.WriteLine($"First {firstIndex - firstRun.Start}   Second {secondIndex - secondRun.Start}");
                //Console.WriteLine($"{firstIndex} After \n{first}\n{second}");
                //Console.WriteLine($"Temporary S({tempStartIndex}) C({tempCurrentIndex}) L({tempLength}) V({temp})");
                //if (!IsSorted(list, firstRun.Start, firstRun.Start - firstIndex - 1))
                //    Console.WriteLine("Sort error");
            }

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }

        private void SortTemporary(IList<T> list, int tempStartIndex, int tempLength, int tempCurrentIndex)
        {
            if (tempCurrentIndex == tempStartIndex)
                return;

            var firstIndex = tempStartIndex;
            var secondIndex = tempCurrentIndex;

            var firstLength = tempCurrentIndex - tempStartIndex;
            var simpleForwardMoves = ((tempLength / firstLength) - 1) * firstLength;

            //var temp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
            //Console.WriteLine($"\nTemp \n{temp}");

            var movesLeft = simpleForwardMoves;
            while (movesLeft-- > 0)
                list.Swap(firstIndex++, secondIndex++);

            var rightRunLength = tempLength - simpleForwardMoves - firstLength;
            if (rightRunLength > 0)
            {
                var leftRun = new SortRun(firstIndex, firstLength);
                var rightRun = new SortRun(firstIndex + firstLength, rightRunLength);
                Merge(list, leftRun, rightRun);
            }

            //var stemp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
            //Console.WriteLine($"\nSorted Temp \n{stemp}");
            //if (!IsSorted(list, tempStartIndex, tempLength))
            //    Console.WriteLine("Temp not sorted");
        }

        private static int WrapTempIndex(int index, int start, int max)
        {
            if (index < start || index > max)
                return start;
            return index;
        }

        //public bool IsSorted(IList<T> list, int start, int length)
        //{
        //    for (int i = start; i < length - 1; i++)
        //    {
        //        var first = list[i];
        //        var second = list[i + 1];

        //        int comparassion = Compare(first, second);
        //        if (comparassion > 0)
        //            return false;
        //    }
        //    return true;
        //}
    }
}
