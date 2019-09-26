using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Logic
{
    public class SortingResult<T>
    {
        public bool Valid { get; }
        public int WriteCount { get; }
        public int ReadCount { get; }
        public float TimeSpent { get; }
        public IReadOnlyList<T> SortedList { get; }

        public SortingResult()
        {
            Valid = false;
            WriteCount = 0;
            ReadCount = 0;
            TimeSpent = 0;
            SortedList = new List<T>();
        }

        public SortingResult(int writeCount, int readCount, float timeSpent, IReadOnlyList<T> sortedList)
        {
            Valid = true;
            WriteCount = writeCount;
            ReadCount = readCount;
            TimeSpent = timeSpent;
            SortedList = sortedList;
        }
    }
}
