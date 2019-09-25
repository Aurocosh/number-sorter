using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Algorhythm
{
    public interface ISortingContainer
    {
        int Count { get; }

        // negative if first is less then second
        // 0 if first and second are equal
        // positive if second is less then first
        int Compare(int first, int second);
        void Swap(int first, int second);
        void Clear();
    }
}
