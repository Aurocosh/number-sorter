using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.Benchmark.IntegerGenerators;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class DualPivotQuickSortCutoffInsertionBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var pivotSelector = new RandomPivotSelectorFactory(BenchmarkRandomProvider.Random);
            var cutoffSort = new InsertionSortFactory();
            return new DualPivotQuickSortFactory(32, cutoffSort, pivotSelector);
        }
    }
}
