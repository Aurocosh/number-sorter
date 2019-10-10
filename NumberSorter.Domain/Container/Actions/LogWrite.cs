using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogWrite<T> : LogAction<T> where T : IEquatable<T>
    {
        public int Index { get; }
        public T Value { get; }

        public LogWrite(int actionIndex, int index, T value) : base(actionIndex, LogActionType.LogWrite)
        {
            Index = index;
            Value = value;
        }

        public override string ToString() => $"Value {Value} written to index {Index}.";

        public override SortState<T> TransformState(SortState<T> state)
        {
            var stateList = new List<T>(state.State);
            stateList[Index] = Value;
            return new SortState<T>(stateList, -1, Index, -1, -1, -1);
        }
    }
}
