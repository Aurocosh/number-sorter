namespace NumberSorter.Domain.Logic
{
    public enum DistributionAlgorhythmType
    {
        BitLSDRadixSort,
        BitLSDOptimizedRadixSort,

        BitMSDRadixSort,
        BitMSDOptimizedRadixSort,

        AmericanFlagSort,
        AverageBucketSort,

        LSDRadixSortBase2,
        LSDRadixSortBase4,
        LSDRadixSortBase16,

        MSDRadixSortBase2,
        MSDRadixSortBase4,
        MSDRadixSortBase16,
    }
}
