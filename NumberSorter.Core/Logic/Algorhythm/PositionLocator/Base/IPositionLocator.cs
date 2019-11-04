using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base
{
    public interface IPositionLocator<T> : IComparingAlgorhythm<T>
    {
        int FindFirstPosition(IList<T> list, T element, int runStart, int length);
        int FindLastPosition(IList<T> list, T element, int runStart, int length);
    }
}
