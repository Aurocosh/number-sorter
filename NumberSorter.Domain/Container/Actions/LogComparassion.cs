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
        public T FirstValue { get; }
        public T SecondValue { get; }
        public int ComparassionResult { get; }

        public override int ComparassionCount => 1;
        public override int FirstComparedIndex { get; }
        public override int SecondComparedIndex { get; }


        public LogComparassion(int actionIndex, int firstComparedIndex, int secondComparedIndex, T firstValue, T secondValue, int comparassionResult) : base(actionIndex, LogActionType.LogComparassion)
        {
            FirstComparedIndex = firstComparedIndex;
            SecondComparedIndex = secondComparedIndex;
            FirstValue = firstValue;
            SecondValue = secondValue;
            ComparassionResult = comparassionResult;
        }

        public override string ToString() => $"Element {FirstValue} ({FirstComparedIndex}) compared to {SecondValue} ({SecondComparedIndex}). Result: {ComparassionResult}.";
    }
}
