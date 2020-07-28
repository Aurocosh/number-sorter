using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class IntervalMultiMergeSortGroupBiasedBinaryBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var runSort = new BinarySortFactory();
            var grouping = new GroupingRunLocatorFactory(32, new BinarySortFactory());
            var positionLocator = new BiasedBinaryPositionLocatorFactory(8);
            return new IntervalMultiMergeSortFactory(runSort, grouping, positionLocator);
        }
    }
}
