using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators.Processors.Converters
{
    public class RandomizeValuesProcessor : IListProcessor
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string Description => "List value randomizer";

        public RandomizeValuesProcessor()
        {
            Minimum = 0;
            Maximum = 0;
        }

        public RandomizeValuesProcessor(int minValue, int maxValue)
        {
            Minimum = minValue;
            Maximum = maxValue;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            ListUtility.Randomize(list, Minimum, Maximum, context.Random);
        }

        public object Clone()
        {
            return new RandomizeValuesProcessor(Minimum, Maximum);
        }
    }
}