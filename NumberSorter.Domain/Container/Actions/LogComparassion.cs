using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogComparassion<T> : LogAction<T> where T : IEquatable<T>
    {
        public int FirstIndex { get; }
        public int SecondIndex { get; }

        public T FirstValue { get; }
        public T SecondValue { get; }

        public int ComparassionResult { get; }

        public LogComparassion(int firstIndex, int secondIndex, T firstValue, T secondValue, int comparassionResult)
        {
            FirstIndex = firstIndex;
            SecondIndex = secondIndex;
            FirstValue = firstValue;
            SecondValue = secondValue;
            ComparassionResult = comparassionResult;
        }

        public override string ToString() => $"Element {FirstValue} ({FirstIndex}) compared to {SecondValue} ({SecondIndex}). Result: {ComparassionResult}.";
    }
}
