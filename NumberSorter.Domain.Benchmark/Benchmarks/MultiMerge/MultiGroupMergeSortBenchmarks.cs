using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SortRunLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class MultiGroupMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new MultiMergeSort<int>(comparer, new GroupingRunLocator<int>(comparer, new InsertionSort<int>(comparer)), x => new InsertionSort<SortRun>(x));
        }
    }
}
