using NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator
{
    public class OnesAndZerosSortBitMaskGenerator : ISortBitMaskGenerator
    {
        public int GenerateMask(IList<int> list, int startingIndex, int length)
        {
            int zeroMask = 0;
            int oneMask = ~zeroMask;
            int indexLimit = startingIndex + length;
            for (int listIndex = startingIndex; listIndex != indexLimit; listIndex++)
            {
                var value = list[listIndex];
                zeroMask |= value;
                oneMask &= value;
            }

            return zeroMask & ~oneMask;
        }
    }
}
