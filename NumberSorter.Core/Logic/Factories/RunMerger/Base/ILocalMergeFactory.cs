using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge.Base
{
    public interface IRunMergerFactory
    {
        IRunMerger GetMerger<T>(IComparer<T> comparer, IList<T> list);
    }
}
