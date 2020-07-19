using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base
{
    public abstract class RecursiveGapGenerator : IGapGenerator
    {
        public int[] GenerateGaps(int arraySize)
        {
            var gaps = new List<int>(16);

            int gap = 1;
            int index = 0;
            int maxGap = MaxGap(arraySize);
            while (gap < maxGap)
            {
                gaps.Add(gap);
                gap = GetNext(index++, gap);
            }
            return gaps.ToArray();
        }

        protected virtual int MaxGap(int arraySize) => arraySize;
        protected abstract int GetNext(int index, int previousValue);
    }
}
