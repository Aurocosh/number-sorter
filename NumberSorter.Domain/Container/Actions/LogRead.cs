using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogRead<T> : LogAction<T> where T : IEquatable<T>
    {
        public int Index { get; }
        public T Value { get; }

        public LogRead(int actionIndex, int index, T value) : base(actionIndex, LogActionType.LogRead)
        {
            Index = index;
            Value = value;
        }

        public override string ToString() => $"Value {Value} read from index {Index}.";

        public override SortState<T> TransformState(SortState<T> state)
        {
            return new SortState<T>(state.State, Index, -1, -1, -1);
        }
    }
}
