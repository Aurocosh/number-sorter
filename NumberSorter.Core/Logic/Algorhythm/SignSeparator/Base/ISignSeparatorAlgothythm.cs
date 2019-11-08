using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base
{
    public interface ISignSeparatorAlgothythm
    {
        // Separates all positive and negative numbers in range.
        // Negative moved to the begining of range, and positive to end.
        // Returns count of negative numbers.
        int Separate(IList<int> list, int startingIndex, int length);
    }
}
