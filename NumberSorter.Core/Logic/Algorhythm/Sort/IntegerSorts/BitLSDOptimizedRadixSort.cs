using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class BitLSDOptimizedRadixSort : IIntegerSortAlgorhythm
    {
        private ISignSeparatorAlgothythm SignSeparator { get; }

        public BitLSDOptimizedRadixSort(ISignSeparatorAlgothythm signSeparator)
        {
            SignSeparator = signSeparator;
        }

        public void Sort(IList<int> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<int> list, int startingIndex, int length)
        {
            int negativeLength = SignSeparator.Separate(list, startingIndex, length);
            int positiveLength = length - negativeLength;
            int positiveIndex = startingIndex + negativeLength;

            IntListUtility.InvertNumbers(list, startingIndex, negativeLength);

            SortInts(list, startingIndex, negativeLength);
            SortInts(list, positiveIndex, positiveLength);

            IntListUtility.InvertPartAndNumbers(list, startingIndex, negativeLength);
        }

        private void SortInts(IList<int> list, int startingIndex, int length)
        {
            int mask = 0;
            int indexLimit = startingIndex + length;
            for (int listIndex = startingIndex; listIndex != indexLimit; listIndex++)
                mask |= list[listIndex];

            int[] buffer = new int[length];
            for (int shift = 31; shift != 0; --shift)
            {
                bool needToSort = (mask << shift) < 0;
                if (needToSort)
                {
                    int bufferIndex = 0;
                    for (int listIndex = startingIndex; listIndex != indexLimit; listIndex++)
                    {
                        var value = list[listIndex];
                        bool move = (value << shift) >= 0;
                        if (move)
                            list[listIndex - bufferIndex] = value;
                        else
                            buffer[bufferIndex++] = value;
                    }
                    ListUtility.Copy(buffer, 0, list, indexLimit - bufferIndex, bufferIndex);
                }
            }
        }
    }
}
