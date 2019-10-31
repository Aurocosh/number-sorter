using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class RecursiveMultiMergeSort<T> : MultiMergeSortBase<T>
    {
        public RecursiveMultiMergeSort(IComparer<T> comparer) : base(comparer)
        {
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

            var sort = new RecursiveMultiMergeSort<SortRun>(compar);
            sort.Sort(list);
        }
    }
}
