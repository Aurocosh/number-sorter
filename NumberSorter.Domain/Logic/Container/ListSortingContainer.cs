using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Algorhythm.Container
{
    public class AccessTrackingList<T> : IList<T>
    {
        private int _readCount;
        private int _writeCount;

        private readonly IList<T> _list;

        public AccessTrackingList(IList<T> list)
        {
            _readCount = 0;
            _writeCount = 0;
            _list = new List<T>(list);
        }

        public T this[int index] {
            get {
                _readCount++;
                return _list[index];
            }
            set {
                _writeCount++;
                _list[index] = value;
            }
        }

        public int Count => _list.Count;
        public bool IsReadOnly => _list.IsReadOnly;

        public int ReadCount => _readCount;
        public int WriteCount => _writeCount;

        public void Add(T item)
        {
            _writeCount++;
            _list.Add(item);
        }

        public void ResetCounters()
        {
            _readCount = 0;
            _writeCount = 0;
        }

        public void Clear()
        {
            _list.Clear();
            ResetCounters();
        }

        public void Insert(int index, T item)
        {
            _writeCount++;
            _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            _writeCount++;
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _writeCount++;
            _list.RemoveAt(index);
        }


        public bool Contains(T item) => _list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        public int IndexOf(T item) => _list.IndexOf(item);
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
    }
}
