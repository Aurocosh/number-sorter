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
    public class PartialShuffleValuesProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int ShuffleCount { get; set; }

        private PartialShuffleValuesProcessor ListProcessor { get; }

        public PartialShuffleValuesProcessorLineViewModel(PartialShuffleValuesProcessor listProcessor) : base(listProcessor)
        {
            ShuffleCount = listProcessor.ShuffleCount;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.ShuffleCount)
                .BindTo(ListProcessor, x => x.ShuffleCount);
        }
    }
}
