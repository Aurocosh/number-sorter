using NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator
{
    public class ZerosSortBitMaskGenerator : ISortBitMaskGenerator
    {
        public int GenerateMask(IList<int> list, int startingIndex, int length)
        {
            int mask = 0;
            int indexLimit = startingIndex + length;
            for (int listIndex = startingIndex; listIndex != indexLimit; listIndex++)
                mask |= list[listIndex];
            return mask;
        }
    }
}
