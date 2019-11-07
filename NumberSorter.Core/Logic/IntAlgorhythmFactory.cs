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
                case AlgorhythmType.MSDRadixSortBase2:
                    return new MSDRadixSort(2);
                case AlgorhythmType.MSDRadixSortBase4:
                    return new MSDRadixSort(4);
                case AlgorhythmType.MSDRadixSortBase16:
                    return new MSDRadixSort(16);
                case AlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

                default:
                    return null;
            }
        }
    }
}