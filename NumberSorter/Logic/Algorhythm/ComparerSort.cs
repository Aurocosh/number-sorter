using NumberSorter.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    abstract public class ComparerSort<T> : ISortAlgorhythm<T>
    {
        private readonly IComparer<T> _comparer;

        protected ComparerSort(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public abstract void Sort(IList<T> list);
        public int Compare(T first, T second) => _comparer.Compare(first, second);
    }
}
