using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Domain.Benchmark.IntegerGenerators;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class ParallelQuickSortRandomPivotCutoffInsertionBenchmarks : ComparassionSortBenchmarks
    {
        protected override ISortFactory GetSortFactory()
        {
            var pivotSelector = new RandomPivotSelectorFactory(BenchmarkRandomProvider.Random);
            var cutoffSort = new InsertionSortFactory();
            return new ParallelQuickSortFactory(32, cutoffSort, pivotSelector);
        }
    }
}
