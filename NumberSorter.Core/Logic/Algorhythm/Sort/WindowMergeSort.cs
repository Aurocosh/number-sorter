using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class WindowMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>, ILocalMergeAlgothythm<T>
    {
        private readonly ILocalMergeAlgothythm<T> _localMergeAlgothythm;

        public WindowMergeSort(IComparer<T> comparer) : base(comparer)
        {
            _localMergeAlgothythm = new RecursiveInPlaceMerge<T>(comparer);
        }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRun = new SortRun(startingIndex, length);
            MergeSort(list, sortRun);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            var halvesOfSortRun = SortRunUtility.SplitSortRun(sortRun);
            MergeSort(list, halvesOfSortRun.First);
            MergeSort(list, halvesOfSortRun.Second);

            Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }


        public void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            //var first = SortRunUtility.RunToString(list, firstRun);
            //var second = SortRunUtility.RunToString(list, secondRun);

            //Console.WriteLine($"\n\nFirst ({firstRun.Start},{firstRun.Length}) Second ({secondRun.Start},{secondRun.Length})");
            //Console.WriteLine($"\nStart {first}   {second}");

            if (firstRun.Length + secondRun.Length < 2)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;
            int lastSecondIndex = secondRun.LastIndex;

            int tempLength = 0;
            int tempStartIndex = secondRun.Start;
            int firstSourceIndex = firstRun.Start;

            while (firstIndex != secondIndex && tempStartIndex <= lastSecondIndex)
            {
                //var nextFromFirst = isUsingTemp ? list[tempCurrentIndex] : list[firstIndex];
                //var nextFromSecond = hasSecond ? list[secondIndex].ToString() : "X";

                //first = SortRunUtility.RunToString(list, firstRun, firstIndex, secondIndex);
                //second = SortRunUtility.RunToString(list, secondRun, firstIndex, secondIndex);
                //var temp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
                //Console.WriteLine($"\n\nBefore");
                //Console.WriteLine($"First {nextFromFirst} g({firstIndex}) l({firstIndex - firstRun.Start})   Second {nextFromSecond} g({secondIndex}) l({secondIndex - secondRun.Start})");
                //Console.WriteLine($"{firstIndex} \n{first}\n{second}");
                //Console.WriteLine($"Temporary S({tempStartIndex}) C({tempCurrentIndex}) L({tempLength}) V({temp})");

                bool hasSecond = secondIndex <= lastSecondIndex;
                if (hasSecond && Compare(list, firstSourceIndex, secondIndex) > 0)
                {
                    list.Swap(firstIndex, secondIndex);
                    if (firstSourceIndex > tempStartIndex)
                        SortTemporary(list, tempStartIndex, tempLength, firstSourceIndex);

                    firstSourceIndex = tempStartIndex;
                    secondIndex++;
                    tempLength++;
                }
                else if (firstIndex != firstSourceIndex)
                {
                    list.Swap(firstIndex, firstSourceIndex);
                    firstSourceIndex++;
                    firstSourceIndex = WrapTempIndex(firstSourceIndex, tempStartIndex, tempStartIndex + tempLength - 1);
                }
                else
                {
                    firstSourceIndex++;
                }

                firstIndex++;
                if (firstIndex == tempStartIndex && tempLength > 0)
                {
                    if (firstSourceIndex != tempStartIndex)
                    {
                        SortTemporary(list, tempStartIndex, tempLength, firstSourceIndex);
                        firstSourceIndex = firstIndex;
                    }
                    tempStartIndex += tempLength;
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

        private void SortTemporary(IList<T> list, int tempStartIndex, int tempLength, int firstSourceIndex)
        {
            var firstLength = firstSourceIndex - tempStartIndex;
            var secondLength = tempLength - firstLength;

            var leftRun = new SortRun(tempStartIndex, firstLength);
            var rightRun = new SortRun(firstSourceIndex, secondLength);

            _localMergeAlgothythm.Merge(list, leftRun, rightRun);
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
