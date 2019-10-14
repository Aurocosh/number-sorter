using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators.Processors.Generators
{
    public class ConsecutiveValuesProcessor : IListProcessor
    {
        public int Step { get; set; }
        public int Start { get; set; }
        public string Description => "Consecutive list generator";

        public ConsecutiveValuesProcessor()
        {
            Step = 0;
            Start = 0;
        }

        public ConsecutiveValuesProcessor(int step, int start)
        {
            Step = step;
            Start = start;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            int current = Start;
            int size = list.Length;
            for (int i = 0; i < size; i++)
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
            return new ConsecutiveValuesProcessor(Step, Start);
        }
    }
}