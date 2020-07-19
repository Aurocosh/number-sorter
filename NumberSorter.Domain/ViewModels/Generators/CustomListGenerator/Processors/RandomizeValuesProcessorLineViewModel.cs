using NumberSorter.Core.CustomGenerators.Processors.Converters;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;

namespace NumberSorter.Domain.ViewModels
{
    public class RandomizeValuesProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int Minimum { get; set; }
        [Reactive] public int Maximum { get; set; }

        private RandomizeValuesProcessor ListProcessor { get; }

        public RandomizeValuesProcessorLineViewModel(RandomizeValuesProcessor listProcessor) : base(listProcessor)
        {
            Minimum = listProcessor.Minimum;
            Maximum = listProcessor.Maximum;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.Minimum)
                .Where(x => x > Maximum)
                .Subscribe(x => Maximum = x);
            this.WhenAnyValue(x => x.Maximum)
                .Where(x => x < Minimum)
                .Subscribe(x => Minimum = x);

            this.WhenAnyValue(x => x.Minimum)
                .Subscribe(x => ListProcessor.Minimum = x);
            this.WhenAnyValue(x => x.Maximum)
                .Subscribe(x => ListProcessor.Maximum = x);
        }
    }
}
