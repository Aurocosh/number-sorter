using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class ArrayMergerFactory : IRunMergerFactory
    {
        private readonly ILocalMergeFactory _mergeFactory;

        public ArrayMergerFactory(ILocalMergeFactory mergeFactory)
        {
            _mergeFactory = mergeFactory;
        }

        public IRunMerger GetMerger<T>(IComparer<T> comparer, IList<T> list)
        {
            var merge = _mergeFactory.GetLocalMerge(comparer, list);
            return new ArrayRunMerger<T>(list, merge);
        }
    }
}
