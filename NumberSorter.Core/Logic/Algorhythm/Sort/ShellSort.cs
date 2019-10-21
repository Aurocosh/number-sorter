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
            int indexLimit = startingIndex + length;
            while (gapIndex >= 0)
            {
                int j;
                int gap = gaps[gapIndex];
                for (int i = gap; i != indexLimit; i++)
                {
                    T temp = list[i];
                    for (j = i; j >= gap && Compare(list[j - gap], temp) > 0; j -= gap)
                    {
                        list[j] = list[j - gap];
                    }
                    list[j] = temp;
                }

                gapIndex--;
            }
        }
    }
}
