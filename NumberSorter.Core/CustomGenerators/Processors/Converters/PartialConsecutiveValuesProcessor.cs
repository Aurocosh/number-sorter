using NumberSorter.Core.CustomGenerators.Base;
using System;

namespace NumberSorter.Core.CustomGenerators.Processors.Generators
{
    public class PartialConsecutiveValuesProcessor : IListProcessor
    {
        public int Count { get; set; }
        public int StartingIndex { get; set; }

        public int Step { get; set; }
        public int StartingValue { get; set; }
        public string Description => "Partial consecutive list generator";

        public PartialConsecutiveValuesProcessor()
        {
            Count = 100;
            StartingIndex = 0;

            Step = 1;
            StartingValue = 0;
        }

        public PartialConsecutiveValuesProcessor(int count, int startingIndex, int step, int startingValue)
        {
            Count = count;
            StartingIndex = startingIndex;

            Step = step;
            StartingValue = startingValue;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            int indexLimit = StartingIndex + Count;


            int current = StartingValue;
            for (int i = StartingIndex; i < indexLimit; i++)
            {
                list[i] = current;
                var next = current + Step;
                if (Step > 0)
                    current = Math.Max(current, next);
                else
                    current = Math.Min(current, next);
            }
        }

        public object Clone()
        {
            return new PartialConsecutiveValuesProcessor(Count, StartingIndex, Step, StartingValue);
        }
    }
}