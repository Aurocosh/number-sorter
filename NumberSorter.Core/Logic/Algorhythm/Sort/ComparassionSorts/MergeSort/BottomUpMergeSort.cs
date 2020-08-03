using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BottomUpMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeFactory LocalMergeFactory { get; }

        public BottomUpMergeSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
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

            int totalSize = sortRun.Length;
            int limit = sortRun.IndexLimit;

            var localMerge = LocalMergeFactory.GetLocalMerge(Comparer, list);

            for (int runSize = 1; runSize < totalSize; runSize += runSize)
            {
                int step = runSize + runSize;
                for (int startingIndex = sortRun.FirstIndex; startingIndex < limit - runSize; startingIndex += step)
                {
                    int secondSize = Math.Min(runSize, limit - (startingIndex + runSize));
                    localMerge.Merge(list, new SortRun(startingIndex, runSize), new SortRun(startingIndex + runSize, secondSize));
                }
            }
        }
    }
}
