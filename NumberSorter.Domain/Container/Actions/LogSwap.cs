using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogSwap<T> : LogAction<T> where T : IEquatable<T>
    {
        public T FirstValue { get; }
        public T SecondValue { get; }

        public override int WriteCount => 2;
        public override int FirstWrittenIndex { get; }
        public override int SecondtWrittenIndex { get; }

        public LogSwap(int actionIndex, int firstWrittenIndex, int secondWrittenIndex, T firstValue, T secondValue) : base(actionIndex, LogActionType.LogWrite)
        {
            FirstWrittenIndex = firstWrittenIndex;
            SecondtWrittenIndex = secondWrittenIndex;

            FirstValue = firstValue;
            SecondValue = secondValue;
        }

        public override string ToString() => $"Swapped {FirstValue} ({SecondtWrittenIndex}) with {SecondValue} ({FirstWrittenIndex}).";

        public override void TransformStateArray(T[] stateArray)
        {
            stateArray[FirstWrittenIndex] = FirstValue;
            stateArray[SecondtWrittenIndex] = SecondValue;
        }
    }
}
