using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.SortRunLocator
{
    public class SimpleRunLocator<T> : GenericRunLocator<T>
    {

        public SimpleRunLocator(IComparer<T> comparer) : base(comparer)
        {
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

            return GetSortRun(list, runStart, runLength, previousRunDirection);
        }

        private static SortRun GetSortRun(IList<T> list, int runStart, int runLength, int runDirection)
        {
            var sortRun = new SortRun(runStart, runLength);
            if (runDirection > 0)
                SortRunUtility.InvertRun(list, sortRun);
            return sortRun;
        }
    }
}
