using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.SortRunLocator
{
    public class GroupingRunLocator<T> : GenericRunLocator<T>
    {
        private int MinimalRunLength { get; }
        private IPartialSortAlgorhythm<T> GroupSortAlgorhythm { get; }

        public GroupingRunLocator(IComparer<T> comparer, IPartialSortAlgorhythm<T> groupSortAlgorhythm, int minimalRunLength) : base(comparer)
        {
            MinimalRunLength = minimalRunLength;
            GroupSortAlgorhythm = groupSortAlgorhythm;
        }

        public override SortRun FindNextSortRun(IList<T> list, int runStart, int length)
        {
            int runLength = 0;
            int currentIndex = runStart;

            int previousRunDirection = 0;
            var previousElement = list[runStart];

            int indexLimit = runStart + length;
            while (currentIndex < indexLimit)
            {
                var nextElement = list[currentIndex];
                var runDirection = Math.Sign(Compare(previousElement, nextElement));
                if (previousRunDirection == 0 || previousRunDirection == runDirection)
                {
                    previousRunDirection = runDirection;
                    previousElement = nextElement;
                    currentIndex++;
                    runLength++;
                }
                else
                {
                    break;
                }
            }

            return GetSortRun(list, runStart, runLength, previousRunDirection, length);
        }

        private SortRun GetSortRun(IList<T> list, int runStart, int runLength, int runDirection, int length)
        {
            var sortRun = new SortRun(runStart, runLength);
            if (runDirection > 0)
                SortRunUtility.InvertRun(list, sortRun);
            if (sortRun.Length < MinimalRunLength)
            {
                sortRun = new SortRun(sortRun.Start, Math.Min(MinimalRunLength, length));
                GroupSortAlgorhythm.Sort(list, sortRun.Start, sortRun.Length);
            }
            return sortRun;
        }
    }
}
