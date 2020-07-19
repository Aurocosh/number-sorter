using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;

namespace NumberSorter.Core.CustomGenerators.Processors.Converters
{
    public class ShuffleValuesProcessor : IListProcessor
    {
        public string Description => "List value shuffler";

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            list.Shuffle(context.Random);
        }

        public object Clone()
        {
            return new ShuffleValuesProcessor();
        }
    }
}
