using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class CSharpDefaultSort<T> : GenericSortAlgorhythm<T>
    {
        public CSharpDefaultSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            if (list is List<T> actualList) // cant sort IList<T> with default c# sort method :(
            {
                actualList.Sort(Comparer);
            }
        }
    }
}
