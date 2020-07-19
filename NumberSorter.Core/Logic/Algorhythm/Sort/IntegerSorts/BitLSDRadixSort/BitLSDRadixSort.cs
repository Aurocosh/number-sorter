using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class BitLSDRadixSort : IIntegerSortAlgorhythm
    {
        public void Sort(IList<int> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<int> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + list.Count;
            int[] buffer = new int[list.Count];
            for (int shift = 31; shift > -1; --shift)
            {
                int bufferIndex = 0;
                for (int listIndex = startingIndex; listIndex != indexLimit; listIndex++)
                {
                    var value = list[listIndex];
                    bool move = (value << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        list[listIndex - bufferIndex] = value;
                    else
                        buffer[bufferIndex++] = value;
                }
                ListUtility.Copy(buffer, 0, list, indexLimit - bufferIndex, bufferIndex);
            }
        }
    }
}
