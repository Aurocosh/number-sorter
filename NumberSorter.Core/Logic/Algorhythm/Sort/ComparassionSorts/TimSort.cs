using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class TimSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }   // Used to merge sorted runs
        private ISortAlgorhythm<T> MinrunSortAlgorhythm { get; }  // Used when sorting something smaller then Minrun

        public TimSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory, ISortFactory minrunSortFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
            MinrunSortAlgorhythm = minrunSortFactory.GetSort(comparer);
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
                return Count > 1 && !((Count < 3 || _sortRunC.Length > _sortRunB.Length + _sortRunA.Length) && _sortRunB.Length > _sortRunA.Length);
            }
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            if (list.Count < 32)
            {
                MinrunSortAlgorhythm.Sort(list);
                return;
            }

            var sortRuns = new LinkedList<SortRun>();

            int currentIndex = startingIndex;
            int elementsLeft = length;
            int minimalRunLength = GetMinrun(length);
            while (elementsLeft > 0)
            {
                var sortRun = FindNextSortRun(list, currentIndex);
                if (sortRun.Length < minimalRunLength)
                {
                    sortRun = new SortRun(sortRun.Start, Math.Min(minimalRunLength, elementsLeft));
                    MinrunSortAlgorhythm.Sort(list, sortRun.Start, sortRun.Length);
                }
                sortRuns.AddLast(sortRun);
                elementsLeft -= sortRun.Length;
                currentIndex += sortRun.Length;
            }

            var runStack = new SortRunStack();
            while (sortRuns.Count > 0 || runStack.Count > 2)
            {
                if (sortRuns.Count == 0 || runStack.MergeNeeded())
                {
                    if (runStack.Count < 3)
                    {
                        var X = runStack.Pop();
                        var Y = runStack.Pop();
                        var mergedRun = MergeRuns(list, Y, X);
                        runStack.Push(mergedRun);
                    }
                    else
                    {
                        var X = runStack.Pop();
                        var Y = runStack.Pop();
                        var Z = runStack.Pop();

                        if (Z.Length <= X.Length)
                        {
                            var mergedRun = MergeRuns(list, Z, Y);
                            runStack.Push(mergedRun);
                            runStack.Push(X);
                        }
                        else
                        {
                            var mergedRun = MergeRuns(list, Y, X);
                            runStack.Push(Z);
                            runStack.Push(mergedRun);
                        }
                    }

                }
                else
                {
                    runStack.Push(sortRuns.First.Value);
                    sortRuns.RemoveFirst();
                }
            }

            if (runStack.Count == 2)
            {
                var X = runStack.Pop();
                var Y = runStack.Pop();
                MergeRuns(list, Y, X);
            }
        }

        private SortRun MergeRuns(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            LocalMergeAlgorhythm.Merge(list, leftRun, rightRun);
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
                SortRunUtility.InvertRun(list, sortRun);
            return sortRun;
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
