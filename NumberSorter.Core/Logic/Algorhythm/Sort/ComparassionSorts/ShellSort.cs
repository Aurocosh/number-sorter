using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class ShellSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private readonly IGapGenerator _gapGenerator;

        public ShellSort(IComparer<T> comparer, IGapGenerator gapGenerator) : base(comparer)
        {
            _gapGenerator = gapGenerator;
        }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            var gaps = _gapGenerator.GenerateGaps(length);

            int gapIndex = gaps.Length - 1;
            int upperlimitIndex = startingIndex + length;
            while (gapIndex >= 0)
            {
                int subIndex;
                int gap = gaps[gapIndex];
                int lowerLimitIndex = startingIndex + gap;
                for (int index = lowerLimitIndex; index != upperlimitIndex; index++)
                {
                    T temp = list[index];
                    for (subIndex = index; subIndex >= lowerLimitIndex && Compare(list[subIndex - gap], temp) > 0; subIndex -= gap)
                    {
                        list[subIndex] = list[subIndex - gap];
                    }
                    list[subIndex] = temp;
                }

                gapIndex--;
            }
        }
    }
}
