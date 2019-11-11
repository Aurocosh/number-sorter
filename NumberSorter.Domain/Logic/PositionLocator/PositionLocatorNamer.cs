using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class PositionLocatorNamer
    {
        private static readonly Dictionary<PositionLocatorType, string> _nameDictionary = new Dictionary<PositionLocatorType, string>();

        static PositionLocatorNamer()
        {
            _nameDictionary.Add(PositionLocatorType.Linear, "Linear");
            _nameDictionary.Add(PositionLocatorType.Binary, "Binary");
            _nameDictionary.Add(PositionLocatorType.BiasedBinary, "Biased binary");
        }

        public static string GetName(PositionLocatorType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}