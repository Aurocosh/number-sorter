using NumberSorter.Core.Logic.Algorhythm.LocalMerge;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class DequeMergeFactory : ILocalMergeFactory
    {
        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer, IList<T> list)
        {
            return new DequeMerge<T>(comparer);
        }
    }
}
