using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class BottomUpBufferRotationMergeSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var position = new BiasedBinaryPositionLocatorFactory(1);
            var rotation = new RecursiveInPlaceRotationFactory();
            var merge = new BufferRotationMergeFactory(position, rotation);
            return new BottomUpMergeSort<int>(comparer, merge);
        }
    }
}
