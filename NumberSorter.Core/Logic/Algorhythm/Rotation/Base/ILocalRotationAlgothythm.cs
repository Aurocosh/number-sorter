using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.Merge.Base
{
    public interface ILocalRotationAlgothythm<T>
    {
        void Rotate(IList<T> list, SortRun leftRun, SortRun rightRun);
    }
}
