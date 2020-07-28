using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class BufferRotationIterativeMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var position = new BiasedBinaryPositionLocatorFactory(8);
            var rotation = new RecursiveInPlaceRotationFactory();
            var merge = new BufferRotationMergeFactory(position, rotation);
            return new BottomUpMergeSortFactory(merge);
        }
    }
}
