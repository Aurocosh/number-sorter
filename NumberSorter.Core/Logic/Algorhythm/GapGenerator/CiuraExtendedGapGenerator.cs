using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator
{
    public class CiuraExtendedGapGenerator : RecursiveGapGenerator
    {
        private static readonly int[] _gaps = new int[] { 1, 4, 10, 23, 57, 132, 301, 701, 1750 };
        private static readonly int _maxIndex = _gaps.Length - 1;

        protected override int GetNext(int index, int previousValue)
        {
            if (index < _maxIndex)
                return _gaps[index];
            else
                return (int)(2.25 * previousValue);
        }
    }
}
