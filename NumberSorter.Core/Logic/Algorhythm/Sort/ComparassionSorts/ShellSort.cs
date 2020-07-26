using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class ShellSort<T> : GenericSortAlgorhythm<T>
    {
        private readonly IGapGenerator _gapGenerator;

        public ShellSort(IComparer<T> comparer, IGapGenerator gapGenerator) : base(comparer)
        {
            _gapGenerator = gapGenerator;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var gaps = _gapGenerator.GenerateGaps(length);

            int gapIndex = gaps.Length - 1;
            int indexLimit = startingIndex + length;
            while (gapIndex >= 0)
            {
                int gap = gaps[gapIndex];
                int startingGappedIndex = startingIndex + gap;
                for (int index = startingGappedIndex; index != indexLimit; index++)
                {
                    int reverseIndex;
                    T temp = list[index];
                    for (reverseIndex = index; reverseIndex >= startingGappedIndex && Compare(list[reverseIndex - gap], temp) > 0; reverseIndex -= gap)
                    {
                        list[reverseIndex] = list[reverseIndex - gap];
                    }
                    list[reverseIndex] = temp;
                }

                gapIndex--;
            }
        }
    }
}
