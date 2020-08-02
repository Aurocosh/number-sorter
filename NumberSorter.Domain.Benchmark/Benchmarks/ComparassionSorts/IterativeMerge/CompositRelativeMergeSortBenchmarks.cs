using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class CompositRelativeMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var locator = new CompositPositionLocatorFactory(16);
            var inversedLocator = new ReversedCompositPositionLocatorFactory(16);
            var merge = new RelativeMergeFactory(locator, inversedLocator);
            return new BottomUpMergeSortFactory(merge);
        }
    }
}
