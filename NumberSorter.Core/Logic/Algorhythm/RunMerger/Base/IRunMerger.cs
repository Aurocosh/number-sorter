using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base
{
    public interface IRunMerger
    {
        void Push(SortRun sortRun);
        void ForceMerge();
    }
}
