using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge.Base
{
    public interface ILocalMergeFactory
    {
        ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer);
    }
}
