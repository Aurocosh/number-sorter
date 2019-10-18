using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Container
{
    public sealed class LogGroup
    {
        public Guid Id { get; }
        public DateTime FirstCreated { get; }
        public List<LogSummary> LogSummaries { get; }

        public LogGroup()
        {
            Id = Guid.Empty;
            FirstCreated = DateTime.Now;
            LogSummaries = new List<LogSummary>();
        }

        public LogGroup(Guid id, IEnumerable<LogSummary> logSummaries)
        {
            Id = id;
            LogSummaries = new List<LogSummary>(logSummaries);
            FirstCreated = LogSummaries.Select(x => x.Created).FirstOrDefault();
        }
    }
}
