using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class CocktailShakerSort<T> : GenericSortAlgorhythm<T>
    {
        public CocktailShakerSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int lowerIndex = 0;
            int middleIndex = startingIndex + length / 2;
            int upperIndex = startingIndex + length - 1;
            while (lowerIndex != middleIndex)
            {
                for (int index = lowerIndex; index != upperIndex; index++)
                {
                    if (Compare(list, index, index + 1) > 0)
                        list.Swap(index, index + 1);
                }
                for (int index = upperIndex; index != lowerIndex; index--)
                {
                    if (Compare(list, index, index - 1) < 0)
                        list.Swap(index, index - 1);
                }
                upperIndex--;
                lowerIndex++;
            }
        }
    }
}
