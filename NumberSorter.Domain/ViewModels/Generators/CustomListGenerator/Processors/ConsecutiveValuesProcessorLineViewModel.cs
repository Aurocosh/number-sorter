﻿using NumberSorter.Core.CustomGenerators.Processors.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class ConsecutiveValuesProcessorLineViewModel : ListProcessorViewModel
    {
        [Reactive] public int Step { get; set; }
        [Reactive] public int Start { get; set; }

        private ConsecutiveValuesProcessor ListProcessor { get; }

        public ConsecutiveValuesProcessorLineViewModel(ConsecutiveValuesProcessor listProcessor) : base(listProcessor)
        {
            Step = listProcessor.Step;
            Start = listProcessor.Start;
            ListProcessor = listProcessor;

            this.WhenAnyValue(x => x.Step)
                .Subscribe(x => ListProcessor.Step = x);
            this.WhenAnyValue(x => x.Start)
                .Subscribe(x => ListProcessor.Start = x);
        }
    }
}
