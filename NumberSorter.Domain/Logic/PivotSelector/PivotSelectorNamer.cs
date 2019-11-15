using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class PivotSelectorNamer
    {
        private static readonly Dictionary<PivotSelectorType, string> _nameDictionary = new Dictionary<PivotSelectorType, string>();

        static PivotSelectorNamer()
        {
            _nameDictionary.Add(PivotSelectorType.Last, "Last");
            _nameDictionary.Add(PivotSelectorType.First, "First");
            _nameDictionary.Add(PivotSelectorType.Center, "Center");
            _nameDictionary.Add(PivotSelectorType.Random, "Random pivot");
            _nameDictionary.Add(PivotSelectorType.MedianOfThree, "Median of three");
        }

        public static string GetName(PivotSelectorType type) => _nameDictionary[type];
    }
}