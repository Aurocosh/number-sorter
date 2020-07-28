using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class BitLSDRadixSortBenchmarks : IntegerSortBenchmarks
    {
        protected override IIntegerSortAlgorhythm GetSortAlgorithm()
        {
            return new BitLSDRadixSort();
        }
    }
}
