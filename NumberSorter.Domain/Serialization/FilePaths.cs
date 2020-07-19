using System;
using System.IO;

namespace NumberSorter.Domain.Serialization
{
    internal static class FilePaths
    {
        public static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NumberSorter");
        public static readonly string LogFolder = Path.Combine(AppDataFolder, "SortLogs");
        public static readonly string ColorSetsFolder = Path.Combine(AppDataFolder, "ColorSets");
        public static readonly string GeneratorsFolder = Path.Combine(AppDataFolder, "Generators");
        public static readonly string InputsListsFolder = Path.Combine(AppDataFolder, "InputsLists");
    }
}
