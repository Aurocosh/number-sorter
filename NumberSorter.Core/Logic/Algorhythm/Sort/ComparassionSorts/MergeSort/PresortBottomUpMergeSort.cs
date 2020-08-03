﻿using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
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

        public PresortBottomUpMergeSort(IComparer<T> comparer, ISortFactory smallRunSortFactory, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            SmallRunSortFactory = smallRunSortFactory;
            LocalMergeFactory = localMergeFactory;
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
                    localMerge.Merge(list, new SortRun(startingRunIndex, runSize), new SortRun(startingRunIndex + runSize, secondSize));
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
