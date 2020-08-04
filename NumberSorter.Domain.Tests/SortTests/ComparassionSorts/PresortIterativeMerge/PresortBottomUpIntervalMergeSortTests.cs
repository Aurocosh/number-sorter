using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class PresortBottomUpIntervalMergeSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var sort = new BinaryOptimizedSortFactory();
            var merge = new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(8));
            var rotation = new RecursiveInPlaceRotationFactory();
            return new PresortBottomUpMergeSort<int>(comparer, sort, merge, rotation);
        }
    }
}
