using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.PositionLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class BufferRotationIterativeMergeSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var position = new BiasedBinaryPositionLocatorFactory(8);
            var rotation = new RecursiveInPlaceRotationFactory();
            var merge = new BufferRotationMergeFactory(position, rotation);
            return new BottomUpMergeSort<int>(comparer, merge);
        }
    }
}
