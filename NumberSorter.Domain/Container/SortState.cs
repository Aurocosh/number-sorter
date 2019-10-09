using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class SortState<T>
    {
        public IReadOnlyList<T> State { get; }

        public int ReadIndex { get; }
        public int WrittenIndex { get; }

        public int FirstComparedIndex { get; }
        public int SecondComparedIndex { get; }

        public SortState(IReadOnlyList<T> state)
        {
            State = state;

            ReadIndex = -1;
            WrittenIndex = -1;

            FirstComparedIndex = -1;
            SecondComparedIndex = -1;
        }

        public SortState(IReadOnlyList<T> state, int readIndex, int writtenIndex, int firstComparedIndex, int secondComparedIndex)
        {
            State = state;

            ReadIndex = readIndex;
            WrittenIndex = writtenIndex;

            FirstComparedIndex = firstComparedIndex;
            SecondComparedIndex = secondComparedIndex;
        }
    }
}
