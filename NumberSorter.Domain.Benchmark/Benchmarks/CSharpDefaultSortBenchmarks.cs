using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class CSharpDefaultSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new CSharpDefaultSort<int>(comparer);
        }
    }
}
