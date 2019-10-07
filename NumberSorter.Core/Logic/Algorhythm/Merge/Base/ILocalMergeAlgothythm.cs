using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Algorhythm.Merge.Base
{
    public interface ILocalMergeAlgothythm<T>
    {
        void Merge(IList<T> list, SortRun leftRun, SortRun rightRun);
        int Compare(T first, T second);
        int Compare(IList<T> list, int first, int second);
    }
}
