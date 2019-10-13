using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.CustomGenerators.Processors.Generators;
using NumberSorter.Core.Logic;
using NumberSorter.Domain.Container.Actions.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Media;

namespace NumberSorter.Domain.ViewModels
{
    public class NewConsecutiveListProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int Size { get; set; }
        [Reactive] public int Step { get; set; }
        [Reactive] public int Start { get; set; }

        private NewConsecutiveListProcessor ListProcessor { get; }

        public NewConsecutiveListProcessorLineViewModel(NewConsecutiveListProcessor listProcessor) : base(listProcessor)
        {
            Size = listProcessor.Size;
            Step = listProcessor.Step;
            Start = listProcessor.Start;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.Size)
                .Subscribe(x => ListProcessor.Size = x);
            this.WhenAnyValue(x => x.Step)
                .Subscribe(x => ListProcessor.Step = x);
            this.WhenAnyValue(x => x.Start)
                .Subscribe(x => ListProcessor.Start = x);
        }
    }
}
