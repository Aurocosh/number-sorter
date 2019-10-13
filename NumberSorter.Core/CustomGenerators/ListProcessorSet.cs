using NumberSorter.Core.CustomGenerators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators
{
    public class ListProcessorSet : ICloneable
    {
        public string Name { get; set; }
        public bool IsSameList { get; set; }
        public int MinRepeatValue { get; set; }
        public int MaxRepeatValue { get; set; }
        public List<IListProcessor> ListProcessors { get; }

        public ListProcessorSet()
        {
            Name = "Processor set";
            IsSameList = false;
            MinRepeatValue = 1;
            MaxRepeatValue = 1;
            ListProcessors = new List<IListProcessor>();
        }

        public ListProcessorSet(string name, bool isSameList, int minRepeatValue, int maxRepeatValue, List<IListProcessor> listProcessors)
        {
            Name = name;
            IsSameList = isSameList;
            MinRepeatValue = minRepeatValue;
            MaxRepeatValue = maxRepeatValue;
            ListProcessors = listProcessors;
        }

        public List<int[]> GenerateLists(IConverterContext context)
        {
            int listCount = context.Random.Next(MinRepeatValue, MaxRepeatValue);
            if (IsSameList)
            {
                var list = GenerateList(context);
                return Enumerable.Range(0, listCount).Select(_ => CopyList(list)).ToList();
            }
            else
            {
                return Enumerable.Range(0, listCount).Select(_ => GenerateList(context)).ToList();
            }
        }

        private int[] GenerateList(IConverterContext context)
        {
            var list = Array.Empty<int>();
            foreach (var processor in ListProcessors)
                processor.ConvertList(ref list, context);
            return list;
        }

        private static int[] CopyList(int[] list)
        {
            var copy = new int[list.Length];
            Array.Copy(list, copy, list.Length);
            return copy;
        }

        public object Clone()
        {
            return new ListProcessorSet(Name, IsSameList, MinRepeatValue, MaxRepeatValue, ListProcessors.Select(x => x.Clone()).Cast<IListProcessor>().ToList());
        }
    }
}
