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
        public T Value { get; }

        public override int WriteCount => 1;
        public override int FirstWrittenIndex { get; }

        public LogWrite(int actionIndex, int index, T value) : base(actionIndex, LogActionType.LogWrite)
        {
            FirstWrittenIndex = index;
            Value = value;
        }

        public override string ToString() => $"Value {Value} written to index {FirstWrittenIndex}.";

        public override void TransformStateArray(T[] stateArray)
        {
            stateArray[FirstWrittenIndex] = Value;
        }
    }
}
