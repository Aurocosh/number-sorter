using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class IntervalMergeSortFactory : PartialSortFactory, ILocalMergeFactory
    {
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public IntervalMergeSortFactory(IPositionLocatorFactory positionLocatorFactory)
        {
            PositionLocatorFactory = positionLocatorFactory;
        }

        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer)
        {
            return new IntervalMergeSort<T>(comparer, PositionLocatorFactory);
        }

        public override IPartialSortAlgorhythm<T> GetPatrialSort<T>(IComparer<T> comparer)
        {
            return new IntervalMergeSort<T>(comparer, PositionLocatorFactory);
        }
    }
}
