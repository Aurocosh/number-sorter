using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator.Base
{
    public interface ISortBitMaskGenerator
    {
        // Positive numbers only
        int GenerateMask(IList<int> list, int startingIndex, int length);
    }
}
