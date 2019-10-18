using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Serialization
{
    internal static class FilePaths
    {
        public static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NumberSorter");
        public static readonly string LogFolder = Path.Combine(AppDataFolder, "SortLogs");
        public static readonly string GeneratorFolder = Path.Combine(AppDataFolder, "Generators");
        public static readonly string InputsListsFolder = Path.Combine(AppDataFolder, "InputsLists");
    }
}
