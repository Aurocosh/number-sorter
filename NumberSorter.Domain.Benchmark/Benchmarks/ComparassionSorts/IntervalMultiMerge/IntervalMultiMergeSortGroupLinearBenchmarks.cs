using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class IntervalMultiMergeSortGroupLinearBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var runSort = new InsertionSortFactory();
            var grouping = new SimpleRunLocatorFactory();
            var positionLocator = new LinearPositionLocatorFactory();
            return new IntervalMultiMergeSortFactory(runSort, grouping, positionLocator);
        }
    }
}
