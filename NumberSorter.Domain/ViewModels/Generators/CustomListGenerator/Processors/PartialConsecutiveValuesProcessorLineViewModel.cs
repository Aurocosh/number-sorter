using NumberSorter.Core.CustomGenerators.Processors.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NumberSorter.Domain.ViewModels
{
    public class PartialConsecutiveValuesProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int Count { get; set; }
        [Reactive] public int StartingIndex { get; set; }

        [Reactive] public int Step { get; set; }
        [Reactive] public int StartingValue { get; set; }

        private PartialConsecutiveValuesProcessor ListProcessor { get; }

        public PartialConsecutiveValuesProcessorLineViewModel(PartialConsecutiveValuesProcessor listProcessor) : base(listProcessor)
        {
            Count = listProcessor.Count;
            StartingIndex = listProcessor.StartingIndex;
            Step = listProcessor.Step;
            StartingValue = listProcessor.StartingValue;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.Count)
                .BindTo(ListProcessor, x => x.Count);
            this.WhenAnyValue(x => x.StartingIndex)
                .BindTo(ListProcessor, x => x.StartingIndex);
            this.WhenAnyValue(x => x.Step)
                .BindTo(ListProcessor, x => x.Step);
            this.WhenAnyValue(x => x.StartingValue)
                .BindTo(ListProcessor, x => x.StartingValue);
        }
    }
}
