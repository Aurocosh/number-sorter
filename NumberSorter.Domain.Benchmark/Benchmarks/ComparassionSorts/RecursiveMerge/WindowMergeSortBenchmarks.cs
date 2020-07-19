using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class WindowMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new WindowMergeFactory();
            return new RecursiveMergeSort<int>(comparer, merge);
        }
    }
}
