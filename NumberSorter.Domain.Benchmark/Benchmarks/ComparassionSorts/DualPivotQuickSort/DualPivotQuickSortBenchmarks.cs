using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Domain.Benchmark.IntegerGenerators;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class DualPivotQuickSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new DualPivotQuickSort<int>(comparer, new RandomPivotSelectorFactory(BenchmarkRandomProvider.Random), new InsertionSortFactory(), 0);
        }
    }
}
