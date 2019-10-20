using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class CycleSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public CycleSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;

            // Loop through the array to find cycles to rotate.
            for (int cycleStart = 0; cycleStart != indexLimit; cycleStart++)
            {
                var item = list[cycleStart];

                // Find where to put the item.
                var pos = cycleStart;

                for (int i = cycleStart + 1; i != indexLimit; i++)
                    if (Compare(list[i], item) < 0)
                        pos++;

                // If the item is already there, this is not a cycle.
                if (pos == cycleStart)
                    continue;

                // Otherwise, put the item there or right after any duplicates.
                while (Compare(item, list[pos]) == 0)
                    pos++;

                var temp = list[pos];
                list[pos] = item;
                item = temp;

                // Rotate the rest of the cycle.
                while (pos != cycleStart)
                {
                    // Find where to put the item.
                    pos = cycleStart;

                    for (int i = cycleStart + 1; i != indexLimit; i++)
                        if (Compare(list[i], item) < 0)
                            pos++;

                    // Put the item there or right after any duplicates.
                    while (Compare(item, list[pos]) == 0)
                        pos++;
                    temp = list[pos];
                    list[pos] = item;
                    item = temp;
                }
            }
        }
    }
}
