using NumberSorter.Core.CustomGenerators.Processors.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;

namespace NumberSorter.Domain.ViewModels
{
    public class NewVariableListProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int MinSize { get; set; }
        [Reactive] public int MaxSize { get; set; }

        private NewVariableListProcessor ListProcessor { get; }

        public NewVariableListProcessorLineViewModel(NewVariableListProcessor listProcessor) : base(listProcessor)
        {
            MinSize = listProcessor.MinSize;
            MaxSize = listProcessor.MaxSize;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.MinSize)
                .Where(x => x > MaxSize)
                .Subscribe(x => MaxSize = x);
            this.WhenAnyValue(x => x.MaxSize)
                .Where(x => x < MinSize)
                .Subscribe(x => MinSize = x);

            this.WhenAnyValue(x => x.MinSize)
                .Subscribe(x => ListProcessor.MinSize = x);
            this.WhenAnyValue(x => x.MaxSize)
                .Subscribe(x => ListProcessor.MaxSize = x);
        }
    }
}
