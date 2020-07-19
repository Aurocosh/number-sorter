using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Benchmark.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    internal sealed class Connection
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Role};Password={Password}";
    }

    internal class UploadSettings
    {
        public bool Enabled { get; set; }
        public Connection Connection { get; set; }
    }
}
