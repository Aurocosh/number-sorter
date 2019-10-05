﻿using System.Collections.Generic;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm.QuickSort.PivotSelectors;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class QuickSortMedianThreePivotBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var pivotSelector = new MedianThreePivotSelector<int>();
            return new QuickSort<int>(comparer, pivotSelector);
        }
    }
}
