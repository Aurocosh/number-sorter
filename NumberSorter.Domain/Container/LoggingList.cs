﻿using NumberSorter.Domain.Container.Actions;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container
{
    public class LoggingList<T> : IList<LogValue<T>>, IComparer<LogValue<T>> where T : IEquatable<T>
    {
        private readonly IComparer<T> _comparer;

        private readonly List<T> _startingState;
        private readonly List<LogValue<T>> _list;

        private readonly List<int> _logValueIndexes;
        private readonly List<LogAction<T>> _actionLog;

        public LoggingList(IList<T> list, IComparer<T> comparer)
        {
            _comparer = comparer;

            int index = 0;
            _startingState = new List<T>(list);
            _list = _startingState.Select(x => new LogValue<T>(index++, x)).ToList();

            _logValueIndexes = new List<int>(list.Count);
            _actionLog = new List<LogAction<T>>();

            for (int i = 0; i < _list.Count; i++)
                _logValueIndexes.Add(i);
        }

        public LogValue<T> this[int index] {
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

        public SortLog<T> GetSortLog(float elapsedTime)
        {
            var startingState = new List<T>(_startingState);
            var finalState = _list.Select(x => x.Value).ToList();
            var actionLog = new List<LogAction<T>>(_actionLog);

            return new SortLog<T>(startingState, finalState, actionLog, _comparer, elapsedTime);
        }

        private void LogRead(int index, LogValue<T> item)
        {
            _actionLog.Add(new LogRead<T>(_actionLog.Count, index, item.Value));
            _logValueIndexes[item.Index] = index;
        }

        private void LogWrite(int index, LogValue<T> item)
        {
            _actionLog.Add(new LogWrite<T>(_actionLog.Count, index, item.Value));
        }

        public void Add(LogValue<T> item)
        {
            LogWrite(_list.Count, item);
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
            _startingState.Clear();
            _actionLog.Clear();
            _logValueIndexes.Clear();
        }

        public void Insert(int index, LogValue<T> item)
        {
            LogWrite(index, item);
            _list.Insert(index, item);
        }

        public bool Remove(LogValue<T> item)
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

        public int Compare(LogValue<T> x, LogValue<T> y)
        {
            int firstIndex = _logValueIndexes[x.Index];
            int secondIndex = _logValueIndexes[y.Index];

            T firstValue = x.Value;
            T secondValue = y.Value;

            int comparassionResult = _comparer.Compare(firstValue, secondValue);
            var comparassion = new LogComparassion<T>(_actionLog.Count, firstIndex, secondIndex, firstValue, secondValue, comparassionResult);

            _actionLog.Add(comparassion);
            return comparassionResult;
        }

        public bool Contains(LogValue<T> item) => _list.Contains(item);
        public void CopyTo(LogValue<T>[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public int IndexOf(LogValue<T> item) => _list.IndexOf(item);
        public IEnumerator<LogValue<T>> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
    }
}
