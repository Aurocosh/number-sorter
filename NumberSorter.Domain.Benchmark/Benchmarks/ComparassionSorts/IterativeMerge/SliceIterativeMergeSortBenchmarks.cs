using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class SliceIterativeMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var merge = new SliceMergeFactory();
            return new BottomUpMergeSortFactory(merge);
        }
    }
}
