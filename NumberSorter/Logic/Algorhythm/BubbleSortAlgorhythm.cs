using NumberSorter.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    public class BubbleSortAlgorhythm : ISortAlgorhythm
    {
        public void Sort(ISortingContainer sortingContainer)
        {
            bool fullySorted = false;
            int count = sortingContainer.Count;

            while (!fullySorted)
            {
                fullySorted = true;
                for (int i = 0; i < count - 1; i++)
                {
                    int comparrassion = sortingContainer.Compare(i, i + 1);
                    if (comparrassion == 1)
                    {
                        sortingContainer.Swap(i, i + 1);
                        fullySorted = false;
                    }
                }
            }
        }
    }
}
