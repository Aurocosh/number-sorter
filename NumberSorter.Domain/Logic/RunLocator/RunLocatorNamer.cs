using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class RunLocatorNamer
    {
        private static readonly Dictionary<RunLocatorType, string> _nameDictionary = new Dictionary<RunLocatorType, string>();

        static RunLocatorNamer()
        {
            _nameDictionary.Add(RunLocatorType.Simple, "Simple");
            _nameDictionary.Add(RunLocatorType.GroupedCustom, "Grouped (Custom)");
            _nameDictionary.Add(RunLocatorType.GroupedBinary32, "Grouped (Binary sort, 32)");
            _nameDictionary.Add(RunLocatorType.GroupedInsertion32, "Grouped (Insertion sort, 32)");
        }

        public static string GetName(RunLocatorType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}