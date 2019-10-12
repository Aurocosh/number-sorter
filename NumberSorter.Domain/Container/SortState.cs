using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class SortState<T> where T : IEquatable<T>
    {
        private readonly T[] _stateArray;

        public IReadOnlyList<T> State => _stateArray;

        public int ReadIndex { get; }

        public int FirstWrittenIndex { get; }
        public int SecondWrittenIndex { get; }

        public int ComparassionResult { get; }
        public int FirstComparedIndex { get; }
        public int SecondComparedIndex { get; }

        public int ReadCount { get; }
        public int WriteCount { get; }
        public int ComparassionCount { get; }


        public SortState(T[] state)
        {
            _stateArray = state;

            ReadIndex = -1;

            FirstWrittenIndex = -1;
            SecondWrittenIndex = -1;

            FirstComparedIndex = -1;
            SecondComparedIndex = -1;
        }

        public SortState(T[] stateArray, int readIndex, int firstWrittenIndex, int secondWrittenIndex, int comparassionResult, int firstComparedIndex, int secondComparedIndex, int readCount, int writeCount, int comparassionCount)
        {
            _stateArray = stateArray;

            ReadIndex = readIndex;

            FirstWrittenIndex = firstWrittenIndex;
            SecondWrittenIndex = secondWrittenIndex;

            ComparassionResult = comparassionResult;
            FirstComparedIndex = firstComparedIndex;
            SecondComparedIndex = secondComparedIndex;

            ReadCount = readCount;
            WriteCount = writeCount;
            ComparassionCount = comparassionCount;
        }

        public SortState<T> TransformState(LogAction<T> logAction)
        {
            int readIndex = logAction.ReadIndex;

            int firstWrittenIndex = logAction.FirstWrittenIndex;
            int secondWrittenIndex = logAction.SecondtWrittenIndex;

            int comparassionResult = logAction.ComparassionResult;
            int firstComparedIndex = logAction.FirstComparedIndex;
            int secondComparedIndex = logAction.SecondComparedIndex;

            int readCount = ReadCount + logAction.ReadCount;
            int writeCount = WriteCount + logAction.WriteCount;
            int comparassionCount = ComparassionCount + logAction.ComparassionCount;

            var arrayState = _stateArray;
            if (logAction.WriteCount > 0)
            {
                arrayState = CopyState();
                logAction.TransformStateArray(arrayState);
            }

            return new SortState<T>(arrayState, readIndex, firstWrittenIndex, secondWrittenIndex, comparassionResult, firstComparedIndex, secondComparedIndex, readCount, writeCount, comparassionCount);
        }

        private T[] CopyState()
        {
            var stateCopy = new T[_stateArray.Length];
            Array.Copy(_stateArray, stateCopy, _stateArray.Length);
            return stateCopy;
        }
    }
}
