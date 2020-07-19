using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator
{
    public class CiuraGapGenerator : RecursiveGapGenerator
    {
        private static readonly int[] _gaps = new int[] { 1, 4, 10, 23, 57, 132, 301, 701, 1750 };
        private static readonly int _maxIndex = _gaps.Length - 1;

        protected override int GetNext(int index, int previousValue)
        {
            return index < _maxIndex ? _gaps[index] : int.MaxValue;
        }
    }
}
