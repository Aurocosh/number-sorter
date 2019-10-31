using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class RecursiveMultiMergeSort<T> : MultiMergeSortBase<T>
    {
        private Func<IComparer<SortRun>, ISortRunLocator<SortRun>> SortRunLocatorFactory { get; }

        public RecursiveMultiMergeSort(IComparer<T> comparer, ISortRunLocator<T> sortRunLocator, Func<IComparer<SortRun>, ISortRunLocator<SortRun>> sortRunLocatorFactory) : base(comparer, sortRunLocator)
        {
            SortRunLocatorFactory = sortRunLocatorFactory;
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

            var locator = SortRunLocatorFactory.Invoke(compar);
            var sort = new RecursiveMultiMergeSort<SortRun>(compar, locator, SortRunLocatorFactory);
            sort.Sort(list);
        }
    }
}
