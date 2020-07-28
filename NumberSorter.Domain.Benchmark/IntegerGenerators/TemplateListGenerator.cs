using Newtonsoft.Json;
using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.CustomGenerators.Base;
using System.IO;
using System.Reflection;

namespace NumberSorter.Domain.Benchmark.IntegerGenerators
{
    public class TemplateListGenerator
    {
        private readonly IConverterContext _converterContext;

        public TemplateListGenerator(IConverterContext converterContext)
        {
            _converterContext = converterContext;
        }

        public int[] Generate(string templateName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"NumberSorter.Domain.Benchmark.IntegerGenerators.Custom.Resources.{templateName}.json";
            var resourceFile = ReadResourceFile(assembly, resourceName);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            var generator = JsonConvert.DeserializeObject<CustomListGenerator>(resourceFile, jsonSerializerSettings);
            return generator.GenerateList(_converterContext);
        }

        private static string ReadResourceFile(Assembly assembly, string filename)
        {
            using (var stream = assembly.GetManifestResourceStream(filename))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}