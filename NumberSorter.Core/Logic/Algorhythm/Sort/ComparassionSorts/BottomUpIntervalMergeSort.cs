using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BottomUpIntervalMergeSort<T> : GenericSortAlgorhythm<T>, ILocalMergeAlgothythm<T>
    {
        private IPositionLocator<T> PositionLocator { get; }

        public BottomUpIntervalMergeSort(IComparer<T> comparer, IPositionLocatorFactory positionLocatorFactory) : base(comparer)
        {
            PositionLocator = positionLocatorFactory.GetPositionLocator(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRun = new SortRun(startingIndex, length);
            MergeSort(list, sortRun);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            int listSize = list.Count;
            int limit = sortRun.FirstIndex + list.Count;

            for (int runSize = 1; runSize < listSize; runSize += runSize)
            {
                int step = runSize + runSize;
                for (int startingIndex = sortRun.FirstIndex; startingIndex < limit - runSize; startingIndex += step)
                {
                    int secondSize = Math.Min(runSize, listSize - (startingIndex + runSize));
                    Merge(list, new SortRun(startingIndex, runSize), new SortRun(startingIndex + runSize, secondSize));
                }
            }


            //var halvesOfSortRun = SortRunUtility.SplitSortRun(sortRun);
            //MergeSort(list, halvesOfSortRun.First);
            //MergeSort(list, halvesOfSortRun.Second);

        }

        public void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            int combinedLength = firstRun.Length + secondRun.Length;
            if (combinedLength < 2)
                return;
            int edgeComparassion = Compare(list, firstRun.LastIndex, secondRun.FirstIndex);
            if (edgeComparassion <= 0)
                return;

            if (combinedLength == 2 && edgeComparassion > 0)
            {
                list.Swap(firstRun.FirstIndex, secondRun.FirstIndex);
                return;
            }

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int unsortedInFirst = firstRun.Length;
            int unsortedInSecond = secondRun.Length;

            T nextFromSecond = list[secondIndex];
            int firstPosition = PositionLocator.FindLastPosition(list, nextFromSecond, firstIndex, unsortedInFirst);

            int skipCount = firstPosition - firstIndex + 1;
            firstIndex += skipCount;
            unsortedInFirst -= skipCount;

            int temporaryIndex = 0;
            var temporaryArray = list.GetRangeAsArray(firstIndex, unsortedInFirst);

            while (true)
            {
                T nextFromFirst = temporaryArray[temporaryIndex];
                int secondPosition = PositionLocator.FindLastPosition(list, nextFromFirst, secondIndex, unsortedInSecond);
                if (secondPosition >= secondIndex)
                {
                    int copyCount = secondPosition - secondIndex + 1;
                    ListUtility.Copy(list, secondIndex, list, firstIndex, copyCount);
                    firstIndex += copyCount;
                    secondIndex += copyCount;
                    unsortedInSecond -= copyCount;
                    if (unsortedInSecond == 0)
                        break;
                }

                nextFromSecond = list[secondIndex];
                firstPosition = PositionLocator.FindLastPosition(temporaryArray, nextFromSecond, temporaryIndex, unsortedInFirst);
                if (firstPosition >= temporaryIndex)
                {
                    int copyCount = firstPosition - temporaryIndex + 1;
                    ListUtility.Copy(temporaryArray, temporaryIndex, list, firstIndex, copyCount);
                    firstIndex += copyCount;
                    temporaryIndex += copyCount;
                    unsortedInFirst -= copyCount;
                    if (unsortedInFirst == 0)
                        break;
                }
            }

            ListUtility.Copy(temporaryArray, temporaryIndex, list, firstIndex, unsortedInFirst);

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }
    }
}
