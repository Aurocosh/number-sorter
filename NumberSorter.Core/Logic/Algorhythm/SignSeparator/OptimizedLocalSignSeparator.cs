using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm.SignSeparator
{
    public class OptimizedLocalSignSeparator : ISignSeparatorAlgothythm
    {
        public int Separate(IList<int> list, int startingIndex, int length)
        {
            int lastIndex = startingIndex + length - 1;

            int leftIndex = startingIndex;
            int rightIndex = lastIndex;

            while (leftIndex <= rightIndex)
            {
                while (leftIndex < lastIndex && list[leftIndex] < 0)
                    leftIndex++;
                while (rightIndex >= startingIndex && list[rightIndex] >= 0)
                    rightIndex--;
                if (leftIndex <= rightIndex)
                    list.Swap(leftIndex++, rightIndex--);
            }

            return leftIndex - startingIndex;
        }
    }
}
