using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SortRunLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class RecursiveSimpleMultiMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new RecursiveMultiMergeSort<int>(comparer, new SimpleRunLocator<int>(comparer), x => new SimpleRunLocator<SortRun>(x));
        }
    }
}
