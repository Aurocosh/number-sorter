using Newtonsoft.Json;
using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.CustomGenerators.Context;
using NumberSorter.Core.Generators;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NumberSorter.Domain.Benchmark.IntegerGenerators
{
    public class SortBenchmark_Custom_DynamicListGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data;

        public SortBenchmark_Custom_DynamicListGenerator(string type)
        {
            _data = GetCustomGeneratorsFromJson(type);
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static List<object[]> GetCustomGeneratorsFromJson(string type)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourses = assembly.GetManifestResourceNames();

            var myRegex = new Regex(@".*\.IntegerGenerators\.Custom.Resources\." + type + @"\..*\.json");
            var generatorTemplates = resourses.Where(x => myRegex.IsMatch(x));
            var generatorJsons = generatorTemplates.Select(x => ReadResourceFile(assembly, x));

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            var generators = generatorJsons.Select(x => JsonConvert.DeserializeObject<CustomListGenerator>(x, jsonSerializerSettings));

            var context = new CustomConverterContext(BenchmarkRandomProvider.Random);

            return generators
                .Select(x => new { generator = x, list = x.GenerateList(context) })
                .Select(x => new object[] { x.list.Length, x.list }).ToList();
        }

        private static string ReadResourceFile(Assembly assembly, string filename)
        {
            using (var stream = assembly.GetManifestResourceStream(filename))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}