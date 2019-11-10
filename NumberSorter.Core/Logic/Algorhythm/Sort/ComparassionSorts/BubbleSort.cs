using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BubbleSort<T> : GenericSortAlgorhythm<T>
    {
        public BubbleSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            bool fullySorted = false;
            int lastIndex = startingIndex + length - 1;
            while (!fullySorted)
            {
                fullySorted = true;
                for (int index = startingIndex; index < lastIndex; index++)
                {
                    var first = list[index];
                    var second = list[index + 1];

                    int comparrassion = Compare(first, second);
                    if (comparrassion > 0)
                    {
                        list.Swap(index, index + 1);
                        fullySorted = false;
                    }
                }
                lastIndex--;
            }
        }
    }
}
