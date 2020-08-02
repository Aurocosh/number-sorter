using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class BottomUpRelativeMergeSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var locator = new BiasedBinaryPositionLocatorFactory(8);
            var inversedLocator = new ReversedBiasedBinaryPositionLocatorFactory(8);

            return new BottomUpMergeSort<int>(comparer, new RelativeMergeFactory(locator, inversedLocator));
        }
    }
}
