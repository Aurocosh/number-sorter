using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class DualPivotQuickSortCutoffInsertionBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new DualPivotQuickSort<int>(comparer, new InsertionSortFactory(), 32);
        }
    }
}
