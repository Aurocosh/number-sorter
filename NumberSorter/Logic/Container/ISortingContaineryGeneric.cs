using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm
{
    public interface ISortingContainer<T> : ISortingContainer
    {
        T this[int index] { get; set; }
        T[] ToArray();
        List<T> ToList();
    }
}
