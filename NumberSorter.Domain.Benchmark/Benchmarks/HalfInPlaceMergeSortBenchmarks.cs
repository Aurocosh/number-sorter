using System.Collections.Generic;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Domain.Logic.Algorhythm;

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
