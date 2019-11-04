﻿using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class TimSortBinaryIntervalBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new TimSort<int>(comparer, new IntervalMergeSortFactory(new BiasedBinaryPositionLocatorFactory(2)), new BinarySortFactory());
        }
    }
}
