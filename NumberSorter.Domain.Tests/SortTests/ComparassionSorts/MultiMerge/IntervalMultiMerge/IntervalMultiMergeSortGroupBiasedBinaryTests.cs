using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class IntervalMultiMergeSortGroupBiasedBinaryTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new IntervalMultiMergeSort<int>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BiasedBinaryPositionLocatorFactory(8));
        }
    }
}
