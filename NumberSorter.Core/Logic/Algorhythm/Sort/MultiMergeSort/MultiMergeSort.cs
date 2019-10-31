using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class MultiMergeSort<T> : MultiMergeSortBase<T>
    {
        private Func<IComparer<SortRun>, IPartialSortAlgorhythm<SortRun>> RunSortFactory { get; }

        public MultiMergeSort(IComparer<T> comparer, ISortRunLocator<T> sortRunLocator, Func<IComparer<SortRun>, IPartialSortAlgorhythm<SortRun>> runSortFactory) : base(comparer, sortRunLocator)
        {
            RunSortFactory = runSortFactory;
        }

        public override void SortRuns(IList<SortRun> list, IList<T> values, IComparer<T> comparer)
        {
            var compar = Comparer<SortRun>.Create(
                   (x, y) =>
                   {
                       var first = values[x.FirstIndex];
                       var second = values[y.FirstIndex];
                       return comparer.Compare(first, second);
                   }
               );

            var sort = RunSortFactory.Invoke(compar);
            sort.Sort(list);
        }
    }
}
