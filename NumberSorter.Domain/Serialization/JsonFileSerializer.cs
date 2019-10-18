using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace NumberSorter.Domain.Serialization
{
    internal sealed class JsonFileSerializer
    {
        private readonly JsonErrorLogger _jsonErrorLogger;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonFileSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            _jsonErrorLogger = new JsonErrorLogger();
            _jsonSerializerSettings = jsonSerializerSettings;
            _jsonSerializerSettings.Error = _jsonErrorLogger.LogError;
        }

        public bool SaveToJsonFile<T>(string filePath, T objectToSerialize) where T : class
        {
            var direcoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(direcoryPath);

            _jsonErrorLogger.Clear();
            string json = JsonConvert.SerializeObject(objectToSerialize, _jsonSerializerSettings);
            File.WriteAllText(filePath, json);

            return !_jsonErrorLogger.HasErrors;
        }

        public T LoadFromJsonFile<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                return null;
            var json = File.ReadAllText(filePath);

            _jsonErrorLogger.Clear();
            var obj = JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
            return obj;
        }

        public T LoadPartFromJsonFile<T>(string filePath, Func<JObject, JToken> partExtractor) where T : class
        {
            if (!File.Exists(filePath))
                return null;
            var json = File.ReadAllText(filePath);

            var jObject = JObject.Parse(json);
            var jToken = partExtractor.Invoke(jObject);

            var serializer = JsonSerializer.Create(_jsonSerializerSettings);
            _jsonErrorLogger.Clear();
            var value = jToken.ToObject<T>(serializer);
            if (_jsonErrorLogger.HasErrors)
            {
                var errorString = string.Join("\n", _jsonErrorLogger.Errors);
                Console.WriteLine(errorString);
                return null;
            }
            return value;
        }
    }
}
