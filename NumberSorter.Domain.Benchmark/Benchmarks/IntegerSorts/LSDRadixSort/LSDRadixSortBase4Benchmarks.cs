using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class LSDRadixSortBase4Benchmarks : SortBenchmarks
    {
        protected override IIntegerSortAlgorhythm GetIntAlgorhythm()
        {
            return new LSDRadixSort(4, new OptimizedLocalSignSeparator());
        }
    }
}
