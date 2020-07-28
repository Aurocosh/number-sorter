using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class QuickSortMedianThreePivotBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var pivotSelector = new MedianOfThreePivotSelectorFactory();
            var cutoffSort = new InsertionSortFactory();
            return new QuickSortLLFactory(0, cutoffSort, pivotSelector);
        }
    }
}
