using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class DummySort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public DummySort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
        }
    }
}
