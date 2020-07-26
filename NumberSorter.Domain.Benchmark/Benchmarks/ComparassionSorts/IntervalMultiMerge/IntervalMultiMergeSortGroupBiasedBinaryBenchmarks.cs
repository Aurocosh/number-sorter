using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class IntervalMultiMergeSortGroupBiasedBinaryBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new IntervalMultiMergeSort<int>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BiasedBinaryPositionLocatorFactory(8));
        }
    }
}
