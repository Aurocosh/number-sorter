using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Core.CustomGenerators.Base;

namespace NumberSorter.Core.CustomGenerators.Processors.Converters
{
    public class DuplicateValuesProcessor : IListProcessor
    {
        public int DuplicateCount { get; set; }
        public string Description => "List value duplicator";

        public DuplicateValuesProcessor()
        {
            DuplicateCount = 1;
        }

        public DuplicateValuesProcessor(int duplicateCount)
        {
            DuplicateCount = Math.Max(1, duplicateCount);
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            int sourceIndex = 0;
            var duplicateCount = DuplicateCount;

            var newSize = list.Length * DuplicateCount;
            if (newSize < 0) //int overflow
                newSize = int.MaxValue;
            var newList = new int[newSize];

            for (int i = 0; i < newSize; i++)
            {
                newList[i] = list[sourceIndex];
                if (duplicateCount-- < 0)
                {
                    sourceIndex++;
                    duplicateCount = DuplicateCount;
                }
            }
        }

        public object Clone()
        {
            return new DuplicateValuesProcessor(DuplicateCount);
        }
    }
}
