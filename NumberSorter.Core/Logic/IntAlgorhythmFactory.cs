using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;

namespace NumberSorter.Core.Logic
{
    public static class IntAlgorhythmFactory
    {
        public static IIntegerSortAlgorhythm GetAlgorhythm(AlgorhythmType algorhythmType)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.BitMSDRadixSort:
                    return new BitLSDRadixSort();
                case AlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

                case AlgorhythmType.MSDRadixSortBase2:
                    return new MSDRadixSort(2);
                case AlgorhythmType.MSDRadixSortBase4:
                    return new MSDRadixSort(4);
                case AlgorhythmType.MSDRadixSortBase16:
                    return new MSDRadixSort(16);

                default:
                    return null;
            }
        }
    }
}