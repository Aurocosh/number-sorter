using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class TimSortGallopArrayBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var merge = new GallopMergeFactory();
            var merger = new ArrayMergerFactory(merge);
            var minrunSort = new BinarySortFactory();
            return new TimSortFactory(merger, minrunSort);
        }
    }
}
