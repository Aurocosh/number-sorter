using NumberSorter.Core.CustomGenerators.Base;

namespace NumberSorter.Core.CustomGenerators.Processors.Generators
{
    public class NewVariableListProcessor : IListProcessor
    {
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public string Description => "Variable list generator";

        public NewVariableListProcessor()
        {
            MinSize = 0;
            MaxSize = 0;
        }

        public NewVariableListProcessor(int minSize, int maxSize)
        {
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            int size = context.Random.Next(MinSize, MaxSize);
            list = new int[size];
        }

        public object Clone()
        {
            return new NewVariableListProcessor(MinSize, MaxSize);
        }
    }
}