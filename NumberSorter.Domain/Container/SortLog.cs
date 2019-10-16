using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Container.Actions;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class SortLog<T> where T : IEquatable<T>
    {
        public SortSummary Summary { get; }
        public SortState<T> InputState { get; }
        public SortState<T> FinalState { get; }
        public IReadOnlyList<LogAction<T>> ActionLog { get; }

        public SortLog()
        {
            Summary = new SortSummary();
            InputState = new SortState<T>(Array.Empty<T>());
            FinalState = new SortState<T>(Array.Empty<T>());
            ActionLog = new List<LogAction<T>>();
        }

        public SortLog(IReadOnlyList<T> startingState, IReadOnlyList<T> finalState, IReadOnlyList<LogAction<T>> actionLog, IComparer<T> comparer, float elapsedTime, string algorhythmName)
        {
            var fullySorted = ListUtility.IsSorted(finalState, comparer);
            var totalReadCount = actionLog.Sum(x => x.ReadCount);
            var totalWriteCount = actionLog.Sum(x => x.WriteCount);
            var totalComparassionCount = actionLog.Sum(x => x.ComparassionCount);
            Summary = new SortSummary(fullySorted, elapsedTime, algorhythmName, startingState.Count, totalReadCount, totalWriteCount, totalComparassionCount);

            InputState = new SortState<T>(startingState.ToArray());
            FinalState = new SortState<T>(finalState.ToArray());
            ActionLog = actionLog;
        }
    }
}
