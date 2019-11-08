﻿using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.PositionLocator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class MultiMergeSortSimpleBinaryBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new MultiMergeSort<int>(comparer, new BinarySortFactory(), new SimpleRunLocatorFactory(), new BinaryPositionLocatorFactory());
        }
    }
}