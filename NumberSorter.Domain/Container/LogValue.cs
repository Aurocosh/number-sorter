using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class LogValue<T> : IEquatable<LogValue<T>>
    {
        public int Index { get; }
        public T Value { get; }

        public LogValue(int index, T value)
        {
            Index = index;
            Value = value;
        }

        #region Equality

        public bool Equals(LogValue<T> other)
        {
            return other is LogValue<T> value &&
                   Index == value.Index &&
                   Value.Equals(value.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is LogValue<T> value &&
                   Index == value.Index &&
                   Value.Equals(value.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = 1376307821;
            hashCode = (hashCode * -1521134295) + Index.GetHashCode();
            hashCode = (hashCode * -1521134295) + Value.GetHashCode();
            return hashCode;
        }

        #endregion
    }
}
