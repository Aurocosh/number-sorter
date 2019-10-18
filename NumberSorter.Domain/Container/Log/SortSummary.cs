using Newtonsoft.Json;
using System;

namespace NumberSorter.Domain.Container
{
    public sealed class LogSummary
    {
        public DateTime Created { get; }
        public bool FullySorted { get; }
        public float ElapsedTime { get; }
        public string AlgorhythmName { get; }

        public Guid InputId { get; }
        public Guid FinishId { get; }
        public string InputName { get; }

        public int ElementCount { get; }
        public int TotalReadCount { get; }
        public int TotalWriteCount { get; }
        public int TotalComparassionCount { get; }

        public LogSummary()
        {
            Created = DateTime.Now;
            FullySorted = true;
            ElapsedTime = 0;
            AlgorhythmName = "";

            InputName = "Empty";
            InputId = Guid.Empty;
            FinishId = Guid.Empty;

            ElementCount = 0;
            TotalReadCount = 0;
            TotalWriteCount = 0;
            TotalComparassionCount = 0;
        }

        public LogSummary(bool fullySorted, float elapsedTime, string algorhythmName, Guid inputId, string inputName, int elementCount, int totalReadCount, int totalWriteCount, int totalComparassionCount)
        {
            Created = DateTime.Now;
            FullySorted = fullySorted;
            ElapsedTime = elapsedTime;
            AlgorhythmName = algorhythmName;

            InputId = inputId;
            InputName = inputName;
            FinishId = Guid.NewGuid();

            ElementCount = elementCount;
            TotalReadCount = totalReadCount;
            TotalWriteCount = totalWriteCount;
            TotalComparassionCount = totalComparassionCount;
        }

        [JsonConstructor]
        public LogSummary(DateTime created, bool fullySorted, float elapsedTime, string algorhythmName, Guid inputId, Guid finishId, string inputName, int elementCount, int totalReadCount, int totalWriteCount, int totalComparassionCount)
        {
            Created = created;
            FullySorted = fullySorted;
            ElapsedTime = elapsedTime;
            AlgorhythmName = algorhythmName;

            InputId = inputId;
            FinishId = finishId;
            InputName = inputName;

            ElementCount = elementCount;
            TotalReadCount = totalReadCount;
            TotalWriteCount = totalWriteCount;
            TotalComparassionCount = totalComparassionCount;
        }
    }
}
