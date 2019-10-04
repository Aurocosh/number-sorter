using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm.Merge.Base;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    abstract public class GenericMergeAlgorhythm<T> : ILocalMergeAlgothythm<T>
    {
        private readonly IComparer<T> _comparer;

        protected GenericMergeAlgorhythm(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public abstract void Merge(IList<T> list, SortRun leftRun, SortRun rightRun);
        public int Compare(T first, T second) => _comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => _comparer.Compare(list[first], list[second]);
    }
}
