using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Container.Actions;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public SortLog(string inputName, Guid inputId, IReadOnlyList<T> inputState, IReadOnlyList<T> finalState)
        {
            var actionLog = new List<LogAction<T>>(1)
            {
                new LogMarker<T>(0, "Initial state of list"),
            };

            InputState = new SortState<T>(inputState.ToArray());
            FinalState = new SortState<T>(finalState.ToArray());

            Summary = new LogSummary(false, 0, "Unsorted", inputId, inputName, inputState.Count, 0, 0, 0);
            ActionLog = actionLog;
        }

        public SortLog(string inputName, Guid inputId, IReadOnlyList<T> inputState, IReadOnlyList<T> finalState, IReadOnlyList<LogAction<T>> actionLog, IComparer<T> comparer, float elapsedTime, string algorhythmName)
        {
            var totalReadCount = actionLog.Sum(x => x.ReadCount);
            var totalWriteCount = actionLog.Sum(x => x.WriteCount);
            var totalComparassionCount = actionLog.Sum(x => x.ComparassionCount);

            InputState = new SortState<T>(inputState.ToArray());
            FinalState = new SortState<T>(finalState.ToArray());

            var fullySorted = ListUtility.IsSorted(FinalState.State, comparer);
            Summary = new LogSummary(fullySorted, elapsedTime, algorhythmName, inputId, inputName, inputState.Count, totalReadCount, totalWriteCount, totalComparassionCount);

            ActionLog = actionLog;
        }
    }
}
