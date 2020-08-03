using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class PresortGallopMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var sort = new BinaryOptimizedSortFactory();
            var merge = new GallopMergeFactory();
            return new PresortBottomUpMergeSortFactory(sort, merge);
        }
    }
}
