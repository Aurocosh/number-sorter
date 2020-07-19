using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using NumberSorter.Core.Logic.Utility;

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
