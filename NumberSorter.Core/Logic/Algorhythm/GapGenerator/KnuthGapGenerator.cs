using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator
{
    public class KnuthGapGenerator : RecursiveGapGenerator
    {
        protected override int GetNext(int index, int previousValue)
        {
            int value = index + 1;
            return (IntUtility.IntPow(3, (uint)value) - 1) / 2;
        }
    }
}
