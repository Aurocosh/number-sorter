using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class LocalMergeNamer
    {
        private static readonly Dictionary<LocalMergeType, string> _nameDictionary = new Dictionary<LocalMergeType, string>();

        static LocalMergeNamer()
        {
            _nameDictionary.Add(LocalMergeType.IntervalLinearSearch, "Normal (Linear search)");
            _nameDictionary.Add(LocalMergeType.IntervalBinarySearch, "Normal (Binary search)");
            _nameDictionary.Add(LocalMergeType.IntervalBiasedBinarySearch, "Normal (Biased binary search)");
            _nameDictionary.Add(LocalMergeType.Window, "Window");
            _nameDictionary.Add(LocalMergeType.TripleWindow, "Triple window");
        }

        public static string GetName(LocalMergeType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}