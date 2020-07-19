using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class TimSortBinaryIntervalSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new TimSort<int>(comparer, new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(2)), new BinarySortFactory());
        }
    }
}
