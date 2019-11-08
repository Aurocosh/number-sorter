﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class MSDRadixSortBase4Benchmarks : SortBenchmarks
    {
        protected override IIntegerSortAlgorhythm GetIntAlgorhythm()
        {
            return new MSDRadixSort(4, new LocalSignSeparator());
        }
    }
}