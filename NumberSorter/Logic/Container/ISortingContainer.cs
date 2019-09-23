using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm
{
    public interface ISortingContainer<T>
    {
        T this[int index] { get; set; }
        int Count { get; }

        // -1 first is less, 0 first and second are same, 1 second is less
        int Compare(int first, int second);
        void Swap(int first, int second);
        void Clear();
        T[] ToArray();
    }
}
