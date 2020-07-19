using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator.Base
{
    public interface ISortBitMaskGenerator
    {
        // Positive numbers only
        int GenerateMask(IList<int> list, int startingIndex, int length);
    }
}
