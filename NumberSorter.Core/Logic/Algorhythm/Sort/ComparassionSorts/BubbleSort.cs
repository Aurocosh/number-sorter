using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BubbleSort<T> : GenericSortAlgorhythm<T>
    {
        public BubbleSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            bool fullySorted = false;
            int count = list.Count - 1;

            while (!fullySorted)
            {
                fullySorted = true;
                for (int i = 0; i < count; i++)
                {
                    var first = list[i];
                    var second = list[i + 1];

                    int comparrassion = Compare(first, second);
                    if (comparrassion > 0)
                    {
                        list.Swap(i, i + 1);
                        fullySorted = false;
                    }
                }
                count--;
            }
        }
    }
}
