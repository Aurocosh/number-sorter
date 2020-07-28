using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class LSDRadixSortBase16Benchmarks : IntegerSortBenchmarks
    {
        protected override IIntegerSortAlgorhythm GetSortAlgorithm()
        {
            return new LSDRadixSort(16, new OptimizedLocalSignSeparator());
        }
    }
}
