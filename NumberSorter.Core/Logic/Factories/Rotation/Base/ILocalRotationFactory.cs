using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge.Base
{
    public interface ILocalRotationFactory
    {
        ILocalRotationAlgothythm<T> GetLocalRotator<T>(IComparer<T> comparer, IList<T> list);
    }
}
