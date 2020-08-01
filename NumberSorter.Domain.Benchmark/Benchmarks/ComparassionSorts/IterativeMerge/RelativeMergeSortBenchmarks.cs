using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class RelativeMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var locator = new BiasedBinaryPositionLocatorFactory(8);
            var inversedLocator = new InvBiasedBinaryPositionLocatorFactory(8);
            var merge = new RelativeMergeFactory(locator, inversedLocator);
            return new BottomUpMergeSortFactory(merge);
        }
    }
}
