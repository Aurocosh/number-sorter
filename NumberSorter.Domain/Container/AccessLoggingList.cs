using NumberSorter.Domain.Container.Actions;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Container
{
    public sealed class AccessLoggingList<T> : IList<T> where T : IEquatable<T>
    {
        private readonly IComparer<T> _comparer;
        private ValueWrite<T> _previousValueWrite;

        private readonly List<T> _startingState;
        private readonly List<T> _list;

        private readonly List<LogAction<T>> _actionLog;

        public AccessLoggingList(IReadOnlyList<T> list, IComparer<T> comparer)
        {
            _comparer = comparer;
            _startingState = new List<T>(list);
            _list = new List<T>(list);

            _actionLog = new List<LogAction<T>>();
            LogMarker("Initial state of list");
        }

        public T this[int index] {
            get {
                var value = _list[index];
                LogRead(index, value);
                return value;
            }
            set {
                LogWrite(index, value);
                _list[index] = value;
            }
        }

        public int Count => _list.Count;
        public bool IsReadOnly => ((IList)_list).IsReadOnly;

        public SortLog<T> GetSortLog(string inputName, Guid inputId, string algorhythmName, float elapsedTime)
        {
            LogPreviousWrite();
            LogMarker("Final state of list");

            var startingState = new List<T>(_startingState);
            var finalState = _list.Select(x => x).ToList();
            var actionLog = new List<LogAction<T>>(_actionLog);

            return new SortLog<T>(inputName, inputId, startingState, finalState, actionLog, _comparer, elapsedTime, algorhythmName);
        }

        private void LogMarker(string markerText)
        {
            LogPreviousWrite();
            _actionLog.Add(new LogMarker<T>(_actionLog.Count, markerText));
        }

        private void LogRead(int index, T item)
        {
            LogPreviousWrite();
            _actionLog.Add(new LogRead<T>(_actionLog.Count, index, item));
        }

        private void LogWrite(int index, T item)
        {
            var replacedValue = _list[index];
            var valueWrite = new ValueWrite<T>(index, item, replacedValue);

            if (_previousValueWrite == null)
            {
                _previousValueWrite = valueWrite;
            }
            else
            {
                if (_previousValueWrite.ReplacedValue.Equals(valueWrite.WrittenValue) && _previousValueWrite.WrittenValue.Equals(valueWrite.ReplacedValue))
                {
                    _actionLog.Add(new LogSwap<T>(_actionLog.Count, _previousValueWrite.Index, valueWrite.Index, _previousValueWrite.WrittenValue, valueWrite.WrittenValue));
                    _previousValueWrite = null;
                }
                else
                {
                    _actionLog.Add(new LogWrite<T>(_actionLog.Count, _previousValueWrite.Index, _previousValueWrite.WrittenValue));
                    _previousValueWrite = valueWrite;
                }
            }
        }

        private void LogPreviousWrite()
        {
            if (_previousValueWrite != null)
            {
                _actionLog.Add(new LogWrite<T>(_actionLog.Count, _previousValueWrite.Index, _previousValueWrite.WrittenValue));
                _previousValueWrite = null;
            }
        }

        public void Add(T item)
        {
            LogWrite(_list.Count, item);
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
            _startingState.Clear();
            _actionLog.Clear();
            LogMarker("Initial state of list");
        }

        public void Insert(int index, T item)
        {
            LogWrite(index, item);
            _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            int index = _list.IndexOf(item);
            LogWrite(index, item);
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            LogWrite(index, item);
            _list.RemoveAt(index);
        }

        public bool Contains(T item) => _list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public int IndexOf(T item) => _list.IndexOf(item);
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
    }
}
