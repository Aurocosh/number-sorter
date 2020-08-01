using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class IterIntervalIterativeMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var merge = new IntervalMergeFactory(new IterativeBiasedBinaryPositionLocatorFactory(8));
            return new BottomUpMergeSortFactory(merge);
        }
    }
}
