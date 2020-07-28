using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class OpRecursiveMergeSortBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            return new OpRecursiveMergeSortFactory();
        }
    }
}
