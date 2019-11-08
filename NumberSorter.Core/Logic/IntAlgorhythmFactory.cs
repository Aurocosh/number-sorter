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
                case AlgorhythmType.BitLSDRadixSort:
                    return new BitLSDRadixSort();
                case AlgorhythmType.BitLSDOptimizedRadixSort:
                    return new BitLSDOptimizedRadixSort(new LocalSignSeparator());

                case AlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

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