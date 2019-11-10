using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class DistributionAlgorhythmNamer
    {
        private static readonly Dictionary<DistributionAlgorhythmType, string> _nameDictionary = new Dictionary<DistributionAlgorhythmType, string>();

        static DistributionAlgorhythmNamer()
        {
            _nameDictionary.Add(DistributionAlgorhythmType.BitLSDRadixSort, "Bit LSD radix sort");
            _nameDictionary.Add(DistributionAlgorhythmType.BitLSDOptimizedRadixSort, "Bit LSD radix sort (Optimized)");

            _nameDictionary.Add(DistributionAlgorhythmType.BitMSDRadixSort, "Bit MSD radix sort");
            _nameDictionary.Add(DistributionAlgorhythmType.BitMSDOptimizedRadixSort, "Bit MSD radix sort (Optimized)");

            _nameDictionary.Add(DistributionAlgorhythmType.AmericanFlagSort, "American flag sort");
            _nameDictionary.Add(DistributionAlgorhythmType.AverageBucketSort, "Average bucket sort");

            _nameDictionary.Add(DistributionAlgorhythmType.LSDRadixSortBase2, "LSD radix sort (Base 2, Positive and negative separate)");
            _nameDictionary.Add(DistributionAlgorhythmType.LSDRadixSortBase4, "LSD radix sort (Base 4, Positive and negative separate)");
            _nameDictionary.Add(DistributionAlgorhythmType.LSDRadixSortBase16, "LSD radix sort (Base 16, Positive and negative separate)");

            _nameDictionary.Add(DistributionAlgorhythmType.MSDRadixSortBase2, "MSD radix sort (Base 2, Positive and negative separate)");
            _nameDictionary.Add(DistributionAlgorhythmType.MSDRadixSortBase4, "MSD radix sort (Base 4, Positive and negative separate)");
            _nameDictionary.Add(DistributionAlgorhythmType.MSDRadixSortBase16, "MSD radix sort (Base 16, Positive and negative separate)");
        }

        public static string GetName(DistributionAlgorhythmType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}