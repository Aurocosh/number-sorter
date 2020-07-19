using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BottomUpMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }

        public BottomUpMergeSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
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
                    LocalMergeAlgorhythm.Merge(list, new SortRun(startingIndex, runSize), new SortRun(startingIndex + runSize, secondSize));
                }
            }
        }
    }
}
