using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class PresortBottomUpMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private const int _minrun = 32;

        private ISortFactory SmallRunSortFactory { get; }
        private ILocalMergeFactory LocalMergeFactory { get; }
        private ILocalRotationFactory LocalRotationFactory { get; }

        public PresortBottomUpMergeSort(IComparer<T> comparer, ISortFactory smallRunSortFactory, ILocalMergeFactory localMergeFactory, ILocalRotationFactory localRotationFactory) : base(comparer)
        {
            SmallRunSortFactory = smallRunSortFactory;
            LocalMergeFactory = localMergeFactory;
            LocalRotationFactory = localRotationFactory;
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

            int startingIndex = sortRun.FirstIndex;
            int totalSize = sortRun.Length;
            int limit = sortRun.IndexLimit;

            var Presort = SmallRunSortFactory.GetSort(Comparer);
            var localMerge = LocalMergeFactory.GetLocalMerge(Comparer, list);
            var localRotation = LocalRotationFactory.GetLocalRotator(Comparer, list);

            int minrun = GetMinrun(totalSize);

            if (totalSize <= minrun)
            {
                Presort.Sort(list, sortRun.FirstIndex, sortRun.Length);
                return;
            }

            int runCount = totalSize / minrun;
            int lastRunStartingIndex = startingIndex + (minrun * runCount);
            for (int startingRunIndex = startingIndex; startingRunIndex < lastRunStartingIndex; startingRunIndex += minrun)
                Presort.Sort(list, startingRunIndex, minrun);

            int lastRunCount = totalSize - (minrun * runCount);
            Presort.Sort(list, lastRunStartingIndex, lastRunCount);

            for (int runSize = minrun; runSize < totalSize; runSize += runSize)
            {
                int step = runSize + runSize;
                for (int startingRunIndex = sortRun.FirstIndex; startingRunIndex < limit - runSize; startingRunIndex += step)
                {
                    int secondSize = Math.Min(runSize, limit - (startingRunIndex + runSize));
                    var first = new SortRun(startingRunIndex, runSize);
                    var second = new SortRun(startingRunIndex + runSize, secondSize);

                    T firstFromFirst = list[first.FirstIndex];
                    T lastFromSecond = list[second.LastIndex];

                    if (Compare(lastFromSecond, firstFromFirst) < 0)
                    {
                        localRotation.Rotate(list, first, second);
                    }
                    else
                    {
                        localMerge.Merge(list, first, second);
                    }
                }
            }
        }

        private static int GetMinrun(int n)
        {
            int r = 0;
            while (n >= _minrun)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }
    }
}
