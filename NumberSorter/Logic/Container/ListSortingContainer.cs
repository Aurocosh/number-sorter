using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm.Container
{
    public class ListSortingContainer<T> : ISortingContainer<T>
    {
        private readonly List<T> _list;
        private readonly IComparer<T> _comparer;

        public ListSortingContainer(IEnumerable<T> enumerable, IComparer<T> comparer)
        {
            _list = new List<T>(enumerable);
            _comparer = comparer;
        }

        public T this[int index] {
            get => _list[index];
            set => _list[index] = value;
        }

        public int Count => _list.Count;
        public void Clear() => _list.Clear();

        public int Compare(int first, int second)
        {
            return _comparer.Compare(_list[first], _list[second]);
        }

        public void Swap(int first, int second)
        {
            T temp = _list[first];
            _list[first] = _list[second];
            _list[second] = temp;
        }

        public T[] ToArray() => _list.ToArray();
    }
}
