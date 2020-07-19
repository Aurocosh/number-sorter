using NumberSorter.Core.CustomGenerators.Processors.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class NewListProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int Size { get; set; }

        private NewListProcessor ListProcessor { get; }

        public NewListProcessorLineViewModel(NewListProcessor listProcessor) : base(listProcessor)
        {
            Size = listProcessor.Size;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.Size)
                .Subscribe(x => ListProcessor.Size = x);
        }
    }
}
