using NumberSorter.Core.Algorhythm;
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
                    return new AmericanFlagSort(10, new OptimizedLocalSignSeparator());
                case AlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

                case AlgorhythmType.BitLSDRadixSort:
                    return new BitLSDRadixSort();
                case AlgorhythmType.BitLSDOptimizedRadixSort:
                    return new BitLSDOptimizedRadixSort(new OptimizedLocalSignSeparator());

                case AlgorhythmType.BitMSDRadixSort:
                    return new BitMSDRadixSort();
                case AlgorhythmType.BitMSDOptimizedRadixSort:
                    return new BitMSDOptimizedRadixSort(new OptimizedLocalSignSeparator());

                case AlgorhythmType.LSDRadixSortBase2:
                    return new LSDRadixSort(2, new OptimizedLocalSignSeparator());
                case AlgorhythmType.LSDRadixSortBase4:
                    return new LSDRadixSort(4, new OptimizedLocalSignSeparator());
                case AlgorhythmType.LSDRadixSortBase16:
                    return new LSDRadixSort(16, new OptimizedLocalSignSeparator());

                case AlgorhythmType.MSDRadixSortBase2:
                    return new MSDRadixSort(2, new OptimizedLocalSignSeparator());
                case AlgorhythmType.MSDRadixSortBase4:
                    return new MSDRadixSort(4, new OptimizedLocalSignSeparator());
                case AlgorhythmType.MSDRadixSortBase16:
                    return new MSDRadixSort(16, new OptimizedLocalSignSeparator());

                default:
                    return null;
            }
        }
    }
}