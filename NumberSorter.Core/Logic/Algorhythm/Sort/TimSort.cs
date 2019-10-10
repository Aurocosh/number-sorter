﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class TimSort<T> : GenericSortAlgorhythm<T>
    {
        private IPartialSortAlgorhythm<T> _smallSortAlgorhythm; // Used when sorting something smaller then Minrun
        private ILocalMergeAlgothythm<T> _mergeAlgorhythm; // Used to merge sorted runs

        public TimSort(IComparer<T> comparer, Func<IComparer<T>, IPartialSortAlgorhythm<T>> smallSortFactory, Func<IComparer<T>, ILocalMergeAlgothythm<T>> mergeFactory) : base(comparer)
        {
            _smallSortAlgorhythm = smallSortFactory.Invoke(comparer);
            _mergeAlgorhythm = mergeFactory.Invoke(comparer);
        }

        private sealed class SortRunStack
        {
            private readonly LinkedList<SortRun> _sortRuns;

            private SortRun _sortRunA;
            private SortRun _sortRunB;
            private SortRun _sortRunC;

            public SortRunStack()
            {
                _sortRuns = new LinkedList<SortRun>();

                _sortRunA = new SortRun(0, 0);
                _sortRunB = new SortRun(0, 0);
                _sortRunC = new SortRun(0, 0);
            }

            public int Count => _sortRuns.Count;

            public void Push(SortRun sortRun)
            {
                _sortRuns.AddLast(_sortRunC);
                _sortRunC = _sortRunB;
                _sortRunB = _sortRunA;
                _sortRunA = sortRun;
            }

            public SortRun Pop()
            {
                if (_sortRuns.Count == 0)
                    return new SortRun(0, 0);
                var sortRun = _sortRunA;
                _sortRunA = _sortRunB;
                _sortRunB = _sortRunC;
                _sortRunC = _sortRuns.Last.Value;
                _sortRuns.RemoveLast();
                return sortRun;
            }

            public bool MergeNeeded()
            {
                return (_sortRunA.Length > _sortRunB.Length + _sortRunC.Length) && (_sortRunB.Length > _sortRunC.Length);
            }
        }

        public override void Sort(IList<T> list)
        {
            if (list.Count < 64)
            {
                _smallSortAlgorhythm.Sort(list);
                return;
            }

            var sortRuns = new LinkedList<SortRun>();

            int currentIndex = 0;
            int listSize = list.Count;
            int elementsLeft = listSize;
            int minimalRunLength = GetMinrun(listSize);
            while (elementsLeft > 0)
            {
                var sortRun = FindNextSortRun(list, currentIndex);
                if (sortRun.Length < minimalRunLength)
                {
                    sortRun = new SortRun(sortRun.Start, Math.Min(minimalRunLength, elementsLeft));
                    _smallSortAlgorhythm.Sort(list, sortRun.Start, sortRun.Length);
                }
                sortRuns.AddLast(sortRun);
                elementsLeft -= sortRun.Length;
                currentIndex += sortRun.Length;
            }

            var runStack = new SortRunStack();
            while (sortRuns.Count > 0 || runStack.Count > 1)
            {
                if (runStack.MergeNeeded() || sortRuns.Count == 0)
                {
                    var leftSortRun = runStack.Pop();
                    var rightSortRun = runStack.Pop();
                    var mergedRun = MergeRuns(list, leftSortRun, rightSortRun);
                    runStack.Push(mergedRun);
                }
                else
                {
                    runStack.Push(sortRuns.Last.Value);
                    sortRuns.RemoveLast();
                }
            }
        }

        private SortRun MergeRuns(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            _mergeAlgorhythm.Merge(list, leftRun, rightRun);
            return new SortRun(leftRun.Start, leftRun.Length + rightRun.Length);
        }

        private SortRun FindNextSortRun(IList<T> list, int runStart)
        {
            int runLength = 0;
            int listSize = list.Count;
            int currentIndex = runStart;

            int previousRunDirection = 0;
            var previousElement = list[runStart];

            while (currentIndex < listSize)
            {
                var nextElement = list[currentIndex];
                var runDirection = Math.Sign(Compare(previousElement, nextElement));
                if (previousRunDirection == 0 || previousRunDirection == runDirection)
                {
                    previousRunDirection = runDirection;
                    previousElement = nextElement;
                    currentIndex++;
                    runLength++;
                }
                else
                {
                    break;
                }
            }

            return GetSortRun(list, runStart, runLength, previousRunDirection);
        }

        private static SortRun GetSortRun(IList<T> list, int runStart, int runLength, int runDirection)
        {
            var sortRun = new SortRun(runStart, runLength);
            if (runDirection > 0)
                InvertRun(list, sortRun);
            return sortRun;
        }

        private static void InvertRun(IList<T> list, SortRun sortRun)
        {
            int leftIndex = sortRun.Start;
            int rightIndex = sortRun.Start + sortRun.Length - 1;
            int swapCount = sortRun.Length / 2;
            for (int i = 0; i < swapCount; i++)
                list.Swap(leftIndex++, rightIndex--);
        }

        private static int GetMinrun(int n)
        {
            int r = 0;           /* станет 1 если среди сдвинутых битов будет хотя бы 1 ненулевой */
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }
    }
}
