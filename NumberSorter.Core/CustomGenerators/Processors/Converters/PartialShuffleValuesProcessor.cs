using System;
using System.Linq;
using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;

namespace NumberSorter.Core.CustomGenerators.Processors.Converters
{
    public class PartialShuffleValuesProcessor : IListProcessor
    {
        public int ShuffleCount { get; set; }

        public string Description => "List value partial shuffler";

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            int size = list.Length;
            int maxShuffleCount = size / 2;
            int shuffleCount = Math.Min(ShuffleCount, maxShuffleCount);

            var listIndexes = Enumerable.Range(0, size).ToList();
            listIndexes.Shuffle(context.Random);

            for (int i = 0; i < shuffleCount; i++)
            {
                int firstIndex = listIndexes[i];
                int secondIndex = listIndexes[size - i - 1];

                list.Swap(firstIndex, secondIndex);
            }
        }

        public object Clone()
        {
            return new PartialShuffleValuesProcessor();
        }
    }
}
