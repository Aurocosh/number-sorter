using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class ShellSortCiuraBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new ShellSort<int>(comparer, new CiuraGapGenerator());
        }
    }
}
