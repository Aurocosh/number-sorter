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

        public LogWrite(int actionIndex, int index, T value) : base(actionIndex)
        {
            Index = index;
            Value = value;
        }

        public override string ToString() => $"Value {Value} written to index {Index}.";
    }
}
