using NumberSorter.Core.CustomGenerators.Base;

namespace NumberSorter.Core.CustomGenerators.Processors.Converters
{
    public class InvertValuesProcessor : IListProcessor
    {
        public string Description => "List value invertor";

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            for (int i = 0; i < list.Length; i++)
            {
                ref int value = ref list[i];
                value = -value;
            }
        }

        public object Clone()
        {
            return new InvertValuesProcessor();
        }
    }
}
