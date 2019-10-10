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
        public int FirstIndex { get; }
        public int SecondIndex { get; }

        public T FirstValue { get; }
        public T SecondValue { get; }

        public LogSwap(int actionIndex, int firstIndex, int secondIndex, T firstValue, T secondValue) : base(actionIndex, LogActionType.LogWrite)
        {
            FirstIndex = firstIndex;
            SecondIndex = secondIndex;
            FirstValue = firstValue;
            SecondValue = secondValue;
        }

        public override string ToString() => $"Swapped {FirstValue} ({SecondIndex}) with {SecondValue} ({FirstIndex}).";

        public override SortState<T> TransformState(SortState<T> state)
        {
            var stateList = new List<T>(state.State)
            {
                [FirstIndex] = FirstValue,
                [SecondIndex] = SecondValue
            };
            return new SortState<T>(stateList, -1, FirstIndex, SecondIndex, -1, -1);
        }
    }
}
