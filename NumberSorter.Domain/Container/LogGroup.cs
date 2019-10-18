using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Container
{
    public sealed class LogGroup
    {
        public Guid Id { get; }
        public int Size { get; }
        public string Name { get; }
        public DateTime FirstCreated { get; }
        public List<LogSummary> LogSummaries { get; }

        public LogGroup()
        {
            Size = 0;
            Name = "Empty";
            Id = Guid.Empty;
            FirstCreated = DateTime.Now;
            LogSummaries = new List<LogSummary>();
        }

        public LogGroup(Guid id, IEnumerable<LogSummary> logSummaries)
        {
            Id = id;
            LogSummaries = new List<LogSummary>(logSummaries);

            var summary = LogSummaries.OrderBy(x => x.Created).FirstOrDefault();
            if (summary != null)
            {
                Name = summary.InputName;
                Size = summary.ElementCount;
                FirstCreated = summary.Created;
            }
            else
            {
                Size = 0;
                Name = "Empty";
                FirstCreated = new DateTime(2000, 1, 1);
            }
        }
    }
}
