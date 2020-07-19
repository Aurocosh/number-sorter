using System.Linq;
using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;

namespace NumberSorter.Core.CustomGenerators.Processors.Converters
{
    public class IntervalValuesProcessor : IListProcessor
    {
        public int Normal { get; set; }
        public int Inverted { get; set; }
        public int Shuffled { get; set; }
        public bool ShuffleParts { get; set; }

        public string Description => "List interval processor (Normal, Inverted, Shuffled)";

        public IntervalValuesProcessor()
        {
        }

        public IntervalValuesProcessor(int normal, int inverted, int shuffled, bool shuffleParts)
        {
            Normal = normal;
            Inverted = inverted;
            Shuffled = shuffled;
            ShuffleParts = shuffleParts;
        }

        public void ConvertList(ref int[] list, IConverterContext context)
        {
            int size = list.Length;
            int partCount = Normal + Inverted + Shuffled;
            if (size < partCount)
                return;

            int partSize = size / partCount;
            var parts = ListUtility.SplitList(list, partSize).Select(x => x.ToArray()).ToArray();
            var partsWorkArray = parts.ToArray();
            partsWorkArray.Shuffle(context.Random);

            int partIndex = 0;
            for (int i = 0; i < Inverted; i++)
            {
                var part = partsWorkArray[partIndex++];
                ListUtility.InvertPart(part, 0, part.Length);
            }
            for (int i = 0; i < Shuffled; i++)
            {
                var part = partsWorkArray[partIndex++];
                part.Shuffle(context.Random);
            }

            if (ShuffleParts)
                parts.Shuffle(context.Random);
            list = ArrayUtility.JoinArrays(parts);
        }

        public object Clone()
        {
            return new IntervalValuesProcessor(Normal, Inverted, Shuffled, ShuffleParts);
        }
    }
}
