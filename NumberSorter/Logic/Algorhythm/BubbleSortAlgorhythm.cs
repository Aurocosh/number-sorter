using NumberSorter.Algorhythm;
using NumberSorter.Logic.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    public class BubbleSortAlgorhythm : ISortAlgorhythm
    {
        public void Sort<T>(IList<T> list, IComparer<T> comparer)
        {
            bool fullySorted = false;
            int count = list.Count;

            while (!fullySorted)
            {
                fullySorted = true;
                for (int i = 0; i < count - 1; i++)
                {
                    var first = list[i];
                    var second = list[i + 1];

                    int comparrassion = comparer.Compare(first, second);
                    if (comparrassion > 0)
                    {
                        list.Swap(i, i + 1);
                        fullySorted = false;
                    }
                }
            }
        }
    }
}
