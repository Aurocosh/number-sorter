using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class PancakeSort<T> : GenericSortAlgorhythm<T>
    {
        public PancakeSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            if (list.Count < 2)
                return;

            int lowerLimit = startingIndex + 1;
            int upperLimit = startingIndex + length;
            for (int index = upperLimit; index > lowerLimit; index--)
            {
                int highestIndex = startingIndex;
                for (int subIndex = startingIndex; subIndex < index; subIndex++)
                {
                    if (Compare(list, subIndex, highestIndex) > 0)
                        highestIndex = subIndex;
                }

                if (highestIndex == index - 1)
                    continue;

                if (highestIndex > 0)
                    ListUtility.InvertPart(list, startingIndex, highestIndex + 1);
                ListUtility.InvertPart(list, startingIndex, index);
            }
        }
    }
}
