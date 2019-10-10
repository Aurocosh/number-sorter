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

        public int FirstWrittenIndex { get; }
        public int SecondWrittenIndex { get; }

        public int FirstComparedIndex { get; }
        public int SecondComparedIndex { get; }

        public SortState(IReadOnlyList<T> state)
        {
            State = state;

            ReadIndex = -1;

            FirstWrittenIndex = -1;
            SecondWrittenIndex = -1;

            FirstComparedIndex = -1;
            SecondComparedIndex = -1;
        }

        public SortState(IReadOnlyList<T> state, int readIndex, int firstWrittenIndex, int secondtWrittenIndex, int firstComparedIndex, int secondComparedIndex)
        {
            State = state;

            ReadIndex = readIndex;

            FirstWrittenIndex = firstWrittenIndex;
            SecondWrittenIndex = secondtWrittenIndex;

            FirstComparedIndex = firstComparedIndex;
            SecondComparedIndex = secondComparedIndex;
        }
    }
}
