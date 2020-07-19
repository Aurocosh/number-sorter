using NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.SignSeparator
{
    public class LocalSignSeparator : ISignSeparatorAlgothythm
    {
        public int Separate(IList<int> list, int startingIndex, int length)
        {
            int nextPositiveIndex = startingIndex + length - 1;
            int nextUnsortedIndex = startingIndex;
            int elementsLeft = length;
            while (elementsLeft-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                if (nextUnsorted < 0)
                    nextUnsortedIndex++;
                else
                    list.Swap(nextUnsortedIndex, nextPositiveIndex--);
            }
            return nextUnsortedIndex - startingIndex;
        }
    }
}
