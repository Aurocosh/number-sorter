using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base
{
    public abstract class RecursiveGapGenerator : IGapGenerator
    {
        public int[] GenerateGaps(int arraySize)
        {
            var gaps = new List<int>(16);

            int gap = 1;
            while (gap < arraySize)
            {
                gaps.Add(gap);
                gap = GetNext(gap);
            }
            return gaps.ToArray();
        }

        protected abstract int GetNext(int value);
    }
}
