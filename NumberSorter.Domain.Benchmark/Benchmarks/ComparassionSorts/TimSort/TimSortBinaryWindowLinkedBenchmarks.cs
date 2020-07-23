﻿using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.LocalMerge;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class TimSortBinaryWindowLinkedBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new WindowMergeFactory();
            var merger = new LinkedListMergerFactory(merge);
            var minrunSort = new BinarySortFactory();
            return new TimSort<int>(comparer, merger, minrunSort);
        }
    }
}
