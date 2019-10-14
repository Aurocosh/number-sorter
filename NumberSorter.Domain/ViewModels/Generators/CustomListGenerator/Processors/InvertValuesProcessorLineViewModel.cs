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
    public class InvertValuesProcessorLineViewModel : ListProcessorViewModel
    {
        private InvertValuesProcessor ListProcessor { get; }

        public InvertValuesProcessorLineViewModel(InvertValuesProcessor listProcessor) : base(listProcessor)
        {
            ListProcessor = listProcessor;
        }
    }
}
