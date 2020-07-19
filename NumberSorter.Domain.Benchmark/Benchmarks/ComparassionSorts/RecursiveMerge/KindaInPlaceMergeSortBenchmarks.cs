using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class KindaInPlaceMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new KindaInPlaceMergeFactory();
            return new RecursiveMergeSort<int>(comparer, merge);
        }
    }
}
