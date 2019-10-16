using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Serialization
{
    internal sealed class JsonFileSerializer<T> where T : class
    {
        private readonly JsonErrorLogger _jsonErrorLogger;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonFileSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            _jsonErrorLogger = new JsonErrorLogger();
            _jsonSerializerSettings = jsonSerializerSettings;
            _jsonSerializerSettings.Error = _jsonErrorLogger.LogError;
        }

        public bool SaveToJsonFile(string filePath, T objectToSerialize)
        {
            var direcoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(direcoryPath);

            _jsonErrorLogger.Clear();
            string json = JsonConvert.SerializeObject(objectToSerialize, _jsonSerializerSettings);
            File.WriteAllText(filePath, json);

            return !_jsonErrorLogger.HasErrors;
        }

        public T LoadFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            var json = File.ReadAllText(filePath);

            _jsonErrorLogger.Clear();
            return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
        }
    }
}
