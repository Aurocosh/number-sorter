using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class TimSortBinaryIntervalLinkedBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var merge = new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(8));
            var merger = new LinkedListMergerFactory(merge);
            var minrunSort = new BinarySortFactory();
            return new TimSortFactory(merger, minrunSort);
        }
    }
}
