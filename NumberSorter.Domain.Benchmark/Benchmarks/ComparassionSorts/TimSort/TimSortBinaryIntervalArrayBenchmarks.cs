using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class TimSortBinaryIntervalArrayBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(8));
            var merger = new ArrayMergerFactory(merge);
            var minrunSort = new BinarySortFactory();
            return new TimSort<int>(comparer, merger, minrunSort);
        }
    }
}
