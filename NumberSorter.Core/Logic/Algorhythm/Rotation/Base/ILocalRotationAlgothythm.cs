using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Algorhythm.Merge.Base
{
    public interface ILocalRotationAlgothythm<T>
    {
        void Rotate(IList<T> list, SortRun leftRun, SortRun rightRun);
    }
}
