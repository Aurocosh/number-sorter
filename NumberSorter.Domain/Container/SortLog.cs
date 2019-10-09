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
        public float ElapsedTime { get; }
        public bool FullySorted { get; }

        public int TotalReadCount { get; }
        public int TotalWriteCount { get; }
        public int TotalComparassionCount { get; }

        public SortState<T> StartingState { get; }
        public SortState<T> FinalState { get; }
        public IReadOnlyList<LogAction<T>> ActionLog { get; }

        public SortLog()
        {
            ElapsedTime = 0;
            FullySorted = true;

            TotalReadCount = 0;
            TotalWriteCount = 0;
            TotalComparassionCount = 0;

            StartingState = new SortState<T>(new List<T>());
            FinalState = new SortState<T>(new List<T>());
            ActionLog = new List<LogAction<T>>();
        }

        public SortLog(IReadOnlyList<T> startingState, IReadOnlyList<T> finalState, IReadOnlyList<LogAction<T>> actionLog, IComparer<T> comparer, float elapsedTime)
        {
            ElapsedTime = elapsedTime;
            FullySorted = IListUtility.IsSorted(finalState, comparer);

            StartingState = new SortState<T>(startingState);
            FinalState = new SortState<T>(finalState);
            ActionLog = actionLog;

            TotalReadCount = actionLog.Count(x => x is LogRead<T>);
            TotalWriteCount = actionLog.Count(x => x is LogWrite<T>);
            TotalComparassionCount = actionLog.Count(x => x is LogComparassion<T>);
        }
    }
}
