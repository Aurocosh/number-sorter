using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public abstract class ShellSortFactory : GenericSortFactory
    {
        private IGapGenerator GapGenerator { get; }

        protected ShellSortFactory(IGapGenerator gapGenerator)
        {
            GapGenerator = gapGenerator;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new ShellSort<T>(comparer, GapGenerator);
        }
    }
}
