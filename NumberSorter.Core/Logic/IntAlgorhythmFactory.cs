﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator;

namespace NumberSorter.Core.Logic
{
    public static class IntAlgorhythmFactory
    {
        public static IIntegerSortAlgorhythm GetAlgorhythm(AlgorhythmType algorhythmType)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.AmericanFlagSort:
                    return new AmericanFlagSort(10, new LocalSignSeparator());
                case AlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

                case AlgorhythmType.BitLSDRadixSort:
                    return new BitLSDRadixSort();
                case AlgorhythmType.BitLSDOptimizedRadixSort:
                    return new BitLSDOptimizedRadixSort(new LocalSignSeparator());

                case AlgorhythmType.BitMSDRadixSort:
                    return new BitMSDRadixSort();
                case AlgorhythmType.BitMSDOptimizedRadixSort:
                    return new BitMSDOptimizedRadixSort(new LocalSignSeparator());

                case AlgorhythmType.LSDRadixSortBase2:
                    return new LSDRadixSort(2, new LocalSignSeparator());
                case AlgorhythmType.LSDRadixSortBase4:
                    return new LSDRadixSort(4, new LocalSignSeparator());
                case AlgorhythmType.LSDRadixSortBase16:
                    return new LSDRadixSort(16, new LocalSignSeparator());

                case AlgorhythmType.MSDRadixSortBase2:
                    return new MSDRadixSort(2, new LocalSignSeparator());
                case AlgorhythmType.MSDRadixSortBase4:
                    return new MSDRadixSort(4, new LocalSignSeparator());
                case AlgorhythmType.MSDRadixSortBase16:
                    return new MSDRadixSort(16, new LocalSignSeparator());

                default:
                    return null;
            }
        }
    }
}