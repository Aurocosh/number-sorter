using NumberSorter.Core.Logic.Algorhythm.LocalMerge;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class SwapWindowMergeFactory : ILocalMergeFactory
    {
        private int MaxDepth { get; }

        public SwapWindowMergeFactory(int maxDepth)
        {
            MaxDepth = maxDepth;
        }

        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer, IList<T> list)
        {
            return new SwapWindowMerge<T>(comparer, MaxDepth);
        }
    }
}
