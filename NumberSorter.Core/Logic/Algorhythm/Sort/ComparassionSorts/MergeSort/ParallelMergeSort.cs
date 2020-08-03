using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class ParallelIterativeMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private readonly int _minimumParallelSize = 64;
        private readonly ILocalMergeFactory _localMergeFactory;

        public ParallelIterativeMergeSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            _localMergeFactory = localMergeFactory;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRun = new SortRun(startingIndex, length);

            int parallelDepth = (int)Math.Ceiling(Math.Log(Environment.ProcessorCount, 2));
            MergeSortParallel(list, sortRun, parallelDepth);
        }

        private void MergeSortParallel(IList<T> list, SortRun sortRun, int parallelDepth)
        {
            if (sortRun.Length <= 1)
                return;

            var halvesOfSortRun = SortRunUtility.SplitSortRun(sortRun);
            if (parallelDepth > 0 && sortRun.Length > _minimumParallelSize)
            {
                parallelDepth--;
                Parallel.Invoke(
                    () => MergeSortParallel(list, halvesOfSortRun.First, parallelDepth),
                    () => MergeSortParallel(list, halvesOfSortRun.Second, parallelDepth));
            }
            else
            {

                MergeSort(list, halvesOfSortRun.First);
                MergeSort(list, halvesOfSortRun.Second);
            }

            var localMerge = _localMergeFactory.GetLocalMerge(Comparer, list);
            localMerge.Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            int totalSize = sortRun.Length;
            int limit = sortRun.IndexLimit;

            var localMerge = _localMergeFactory.GetLocalMerge(Comparer, list);

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
