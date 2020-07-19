using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base
{
    public interface ILocalMergeAlgothythm<T> : IComparingAlgorhythm<T>
    {
        void Merge(IList<T> list, SortRun leftRun, SortRun rightRun);
    }
}
