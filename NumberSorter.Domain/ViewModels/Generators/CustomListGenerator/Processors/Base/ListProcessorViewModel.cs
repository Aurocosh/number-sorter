using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.Logic;
using NumberSorter.Domain.Container.Actions.Base;
using ReactiveUI;
using System;
using System.Windows.Media;

namespace NumberSorter.Domain.ViewModels
{
    public abstract class ListProcessorViewModel : ReactiveObject
    {
        public IListProcessor IListProcessor { get; }

        protected ListProcessorViewModel(IListProcessor listProcessor)
        {
            IListProcessor = listProcessor;
        }
    }
}
