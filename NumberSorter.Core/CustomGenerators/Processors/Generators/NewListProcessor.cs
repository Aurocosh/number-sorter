using NumberSorter.Core.CustomGenerators.Base;

namespace NumberSorter.Core.CustomGenerators.Processors.Generators
{
    public class NewListProcessor : IListProcessor
    {
        public int Size { get; set; }
        public string Description => "Fixed list generator";

        public NewListProcessor()
        {
            Size = 0;
        }

        public NewListProcessor(int size)
        {
            Size = size;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            list = new int[Size];
        }

        public object Clone()
        {
            return new NewListProcessor(Size);
        }
    }
}