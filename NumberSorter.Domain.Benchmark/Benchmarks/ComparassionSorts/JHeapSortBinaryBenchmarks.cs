using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class JHeapSortBinaryBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var afterSort = new BinarySortFactory();
            return new JHeapSortFactory(afterSort);
        }
    }
}
