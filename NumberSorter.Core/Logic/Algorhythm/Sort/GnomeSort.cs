using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class GnomeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public GnomeSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            int index = 0;
            int indexLimit = startingIndex + length;
            while (index < indexLimit)
            {
                if (index == 0 || Compare(list, index - 1, index) <= 0)
                {
                    index++;
                }
                else
                {
                    list.Swap(index - 1, index);
                    index--;
                }
            }
        }
    }
}
