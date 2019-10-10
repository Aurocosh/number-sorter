using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class LogValueWrite<T> where T : IEquatable<T>
    {
        public int Index { get; }
        public LogValue<T> WrittenValue { get; }
        public LogValue<T> ReplacedValue { get; }

        public LogValueWrite(int index, LogValue<T> writtenValue, LogValue<T> replacedValue)
        {
            Index = index;
            WrittenValue = writtenValue;
            ReplacedValue = replacedValue;
        }
    }
}
