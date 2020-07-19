using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base
{
    abstract public class GenericMergeAlgorhythm<T> : ILocalMergeAlgothythm<T>
    {
        public IComparer<T> Comparer { get; }

        protected GenericMergeAlgorhythm(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public abstract void Merge(IList<T> list, SortRun leftRun, SortRun rightRun);
        public int Compare(T first, T second) => Comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => Comparer.Compare(list[first], list[second]);
    }
}
