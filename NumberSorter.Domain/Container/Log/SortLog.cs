using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Container.Actions;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class SortLog<T> where T : IEquatable<T>
    {
        public LogSummary Summary { get; }
        public SortState<T> InputState { get; }
        public SortState<T> FinalState { get; }
        public IReadOnlyList<LogAction<T>> ActionLog { get; }

        public SortLog()
        {
            Summary = new LogSummary();
            InputState = new SortState<T>(Array.Empty<T>());
            FinalState = new SortState<T>(Array.Empty<T>());
            ActionLog = new List<LogAction<T>>();
        }

        public SortLog(string inputName, Guid inputId, IReadOnlyList<T> inputState, IReadOnlyList<T> finalState, IReadOnlyList<LogAction<T>> actionLog, IComparer<T> comparer, float elapsedTime, string algorhythmName)
        {
            var fullySorted = ListUtility.IsSorted(finalState, comparer);
            var totalReadCount = actionLog.Sum(x => x.ReadCount);
            var totalWriteCount = actionLog.Sum(x => x.WriteCount);
            var totalComparassionCount = actionLog.Sum(x => x.ComparassionCount);
            Summary = new LogSummary(fullySorted, elapsedTime, algorhythmName, inputId, inputName, inputState.Count, totalReadCount, totalWriteCount, totalComparassionCount);

            InputState = new SortState<T>(inputState.ToArray());
            FinalState = new SortState<T>(finalState.ToArray());
            ActionLog = actionLog;
        }
    }
}
