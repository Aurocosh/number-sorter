using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.CustomGenerators.Processors.Converters;
using NumberSorter.Core.CustomGenerators.Processors.Generators;
using NumberSorter.Core.Logic;
using NumberSorter.Domain.Container.Actions.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Media;

namespace NumberSorter.Domain.ViewModels
{
    public class IntervalValuesProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int Normal { get; set; }
        [Reactive] public int Inverted { get; set; }
        [Reactive] public int Shuffled { get; set; }
        [Reactive] public bool ShuffleParts { get; set; }

        private IntervalValuesProcessor ListProcessor { get; }

        public IntervalValuesProcessorLineViewModel(IntervalValuesProcessor listProcessor) : base(listProcessor)
        {
            Normal = listProcessor.Normal;
            Inverted = listProcessor.Inverted;
            Shuffled = listProcessor.Shuffled;
            ShuffleParts = listProcessor.ShuffleParts;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.Normal)
                .BindTo(ListProcessor, x => x.Normal);
            this.WhenAnyValue(x => x.Inverted)
                .BindTo(ListProcessor, x => x.Inverted);
            this.WhenAnyValue(x => x.Shuffled)
                .BindTo(ListProcessor, x => x.Shuffled);
            this.WhenAnyValue(x => x.ShuffleParts)
                .BindTo(ListProcessor, x => x.ShuffleParts);
        }
    }
}
