using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SortRunLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.SortRunLocator
{
    public class GroupingRunLocatorFactory : ISortRunLocatorFactory
    {
        private int MinimalRunLength { get; }
        private ISortFactory GroupSortFactory { get; }

        public GroupingRunLocatorFactory(int minimalRunLength, ISortFactory groupSortFactory)
        {
            MinimalRunLength = minimalRunLength;
            GroupSortFactory = groupSortFactory;
        }

        public ISortRunLocator<T> GetSortRunLocator<T>(IComparer<T> comparer)
        {
            return new GroupingRunLocator<T>(comparer, GroupSortFactory, MinimalRunLength);
        }
    }
}
