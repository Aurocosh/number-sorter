using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class WikiSortFactory : GenericSortFactory
    {
        private int BufferSize { get; }

        public WikiSortFactory(int bufferSize)
        {
            BufferSize = bufferSize;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new WikiSort<T>(comparer, BufferSize);
        }
    }
}
