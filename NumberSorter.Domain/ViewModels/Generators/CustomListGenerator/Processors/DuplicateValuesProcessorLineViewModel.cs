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
    public class DuplicateValuesProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int DuplicateCount { get; set; }

        private DuplicateValuesProcessor ListProcessor { get; }

        public DuplicateValuesProcessorLineViewModel(DuplicateValuesProcessor listProcessor) : base(listProcessor)
        {
            DuplicateCount = listProcessor.DuplicateCount;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.DuplicateCount)
                .Subscribe(x => ListProcessor.DuplicateCount = x);
        }
    }
}
