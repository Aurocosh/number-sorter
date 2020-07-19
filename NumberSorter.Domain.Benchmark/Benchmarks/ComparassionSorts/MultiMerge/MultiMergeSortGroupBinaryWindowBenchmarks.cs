using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class MultiMergeSortGroupBinaryWindowBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var internalSort = new BottomUpMergeSortFactory(new WindowMergeFactory());
            var runLocator = new GroupingRunLocatorFactory(32, new BinarySortFactory());
            var positionLocator = new BinaryPositionLocatorFactory();
            return new MultiMergeSort<int>(comparer, internalSort, runLocator, positionLocator);
        }
    }
}
