using NumberSorter.Core.CustomGenerators;
using ReactiveUI;

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
