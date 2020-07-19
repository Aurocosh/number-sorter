using NumberSorter.Core.CustomGenerators.Processors.Converters;

namespace NumberSorter.Domain.ViewModels
{
    public class ShuffleValuesProcessorLineViewModel : ListProcessorViewModel
    {
        private ShuffleValuesProcessor ListProcessor { get; }

        public ShuffleValuesProcessorLineViewModel(ShuffleValuesProcessor listProcessor) : base(listProcessor)
        {
            ListProcessor = listProcessor;
        }
    }
}
