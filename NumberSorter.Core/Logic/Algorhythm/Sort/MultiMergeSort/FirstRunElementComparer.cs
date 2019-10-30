using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    internal class FirstRunElementComparer<T> : IComparer<SortRun>
    {
        private readonly IList<T> _list;
        private readonly IComparer<T> _comparer;

        public FirstRunElementComparer(IList<T> list, IComparer<T> comparer)
        {
            _list = list;
            _comparer = comparer;
        }

        public int Compare(SortRun x, SortRun y)
        {
            var first = _list[x.FirstIndex];
            var second = _list[y.FirstIndex];
            return _comparer.Compare(first, second);
        }
    }
}