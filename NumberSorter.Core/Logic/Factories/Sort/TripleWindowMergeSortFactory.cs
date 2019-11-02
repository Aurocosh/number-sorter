using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class TripleWindowMergeSortFactory : PartialSortFactory, ILocalMergeFactory
    {
        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer)
        {
            return new TripleWindowMergeSort<T>(comparer);
        }

        public override IPartialSortAlgorhythm<T> GetPatrialSort<T>(IComparer<T> comparer)
        {
            return new TripleWindowMergeSort<T>(comparer);
        }
    }
}
