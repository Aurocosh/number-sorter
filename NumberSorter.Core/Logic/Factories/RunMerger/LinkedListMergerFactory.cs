using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class LinkedListMergerFactory : IRunMergerFactory
    {
        private readonly ILocalMergeFactory _mergeFactory;

        public LinkedListMergerFactory(ILocalMergeFactory mergeFactory)
        {
            _mergeFactory = mergeFactory;
        }

        public IRunMerger GetMerger<T>(IComparer<T> comparer, IList<T> list)
        {
            var merge = _mergeFactory.GetLocalMerge(comparer);
            return new LinkedListRunMerger<T>(list, merge);
        }
    }
}
