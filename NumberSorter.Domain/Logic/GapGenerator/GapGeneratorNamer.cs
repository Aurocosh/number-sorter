using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class GapGeneratorNamer
    {
        private static readonly Dictionary<GapGeneratorType, string> _nameDictionary = new Dictionary<GapGeneratorType, string>();

        static GapGeneratorNamer()
        {
            _nameDictionary.Add(GapGeneratorType.Ciura, "Ciura gaps");
            _nameDictionary.Add(GapGeneratorType.Knuth, "Knuth gaps");
            _nameDictionary.Add(GapGeneratorType.Tokuda, "Tokuda gaps");
        }

        public static string GetName(GapGeneratorType type)
        {
            if (_nameDictionary.TryGetValue(type, out string name))
                return name;
            return "Generator name is unknown";
        }
    }
}