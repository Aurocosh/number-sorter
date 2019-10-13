using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators.Processors.Generators
{
    public class NewConsecutiveListProcessor : IListProcessor
    {
        public int Size { get; set; }
        public int Step { get; set; }
        public int Start { get; set; }
        public string Description => "Consecutive list generator";

        public NewConsecutiveListProcessor()
        {
            Size = 0;
            Step = 0;
            Start = 0;
        }

        public NewConsecutiveListProcessor(int size, int step, int start)
        {
            Size = size;
            Step = step;
            Start = start;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            list = new int[Size];
            int current = Start;

            for (int i = 0; i < Size; i++)
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
            return new NewConsecutiveListProcessor(Size, Step, Start);
        }
    }
}