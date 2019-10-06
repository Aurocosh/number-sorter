using System.Collections.Generic;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Domain.Logic.Algorhythm;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class OpRecursiveMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new OpRecursiveMergeSort<int>(comparer);
        }
    }
}
