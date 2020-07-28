using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class IntervalMergeSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(4));
            //var merge = new IntervalMergeFactory(new BinaryPositionLocatorFactory());
            return new RecursiveMergeSort<int>(comparer, merge);
        }
    }
}
