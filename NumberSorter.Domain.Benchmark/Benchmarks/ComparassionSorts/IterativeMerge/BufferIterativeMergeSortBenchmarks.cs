using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class BufferIterativeMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new BufferMergeFactory(new BiasedBinaryPositionLocatorFactory(8));
            return new BottomUpMergeSort<int>(comparer, merge);
        }
    }
}
