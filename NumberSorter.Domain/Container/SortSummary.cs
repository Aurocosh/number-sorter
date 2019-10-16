using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public sealed class SortSummary
    {
        public bool FullySorted { get; }
        public float ElapsedTime { get; }
        public string AlgorhythmName { get; }

        public int ElementCount { get; }
        public int TotalReadCount { get; }
        public int TotalWriteCount { get; }
        public int TotalComparassionCount { get; }

        public SortSummary()
        {
            FullySorted = true;
            ElapsedTime = 0;
            AlgorhythmName = "";
            TotalReadCount = 0;
            TotalWriteCount = 0;
            TotalComparassionCount = 0;
        }

        public SortSummary(bool fullySorted, float elapsedTime, string algorhythmName, int elementCount, int totalReadCount, int totalWriteCount, int totalComparassionCount)
        {
            FullySorted = fullySorted;
            ElapsedTime = elapsedTime;
            AlgorhythmName = algorhythmName;
            ElementCount = elementCount;
            TotalReadCount = totalReadCount;
            TotalWriteCount = totalWriteCount;
            TotalComparassionCount = totalComparassionCount;
        }
    }
}
