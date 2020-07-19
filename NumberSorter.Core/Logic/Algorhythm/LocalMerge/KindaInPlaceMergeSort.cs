using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class KindaInPlaceMerge<T> : GenericMergeAlgorhythm<T>
    {
        public KindaInPlaceMerge(IComparer<T> comparer) : base(comparer)
        {
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length + secondRun.Length < 2)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int unsortedInFirst = firstRun.Length;
            int unsortedInSecond = secondRun.Length;

            while (Compare(list, firstIndex, secondIndex) <= 0 && unsortedInFirst > 0)
            {
                unsortedInFirst--;
                firstIndex++;
            }

            int temporaryIndex = 0;
            var temporartArray = list.GetRangeAsArray(firstIndex, unsortedInFirst);

            while (unsortedInFirst > 0 && unsortedInSecond > 0)
            {
                if (Compare(temporartArray[temporaryIndex], list[secondIndex]) > 0)
                {
                    list[firstIndex++] = list[secondIndex++];
                    unsortedInSecond--;
                }
                else
                {
                    list[firstIndex++] = temporartArray[temporaryIndex++];
                    unsortedInFirst--;
                }
            }

            while (unsortedInFirst-- > 0)
                list[firstIndex++] = temporartArray[temporaryIndex++];
        }
    }
}
