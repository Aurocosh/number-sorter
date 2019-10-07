using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class HalfInPlaceMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new HalfInPlaceMergeSort<int>(comparer);
        }
    }
}
