using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm
{
    public interface ISortAlgorhythm<T>
    {
        void Sort(ISortingContainer<T> sortingContainer);
    }
}
