using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class InversionInPlaceRotationFactory : ILocalRotationFactory
    {
        public InversionInPlaceRotationFactory()
        {
        }

        public ILocalRotationAlgothythm<T> GetLocalRotator<T>(IComparer<T> comparer, IList<T> list)
        {
            return new InversionInPlaceRotation<T>();
        }
    }
}
