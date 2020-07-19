using NumberSorter.Core.CustomGenerators.Processors.Converters;

namespace NumberSorter.Domain.ViewModels
{
    public class InvertValuesProcessorLineViewModel : ListProcessorViewModel
    {
        private InvertValuesProcessor ListProcessor { get; }

        public InvertValuesProcessorLineViewModel(InvertValuesProcessor listProcessor) : base(listProcessor)
        {
            ListProcessor = listProcessor;
        }
    }
}
