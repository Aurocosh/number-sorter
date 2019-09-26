using NumberSorter.Domain.DialogService;
using ReactiveUI;

namespace NumberSorter.DialogService
{
    public class WindowViewInitializer<TViewModel> : IViewInitializer where TViewModel : class
    {
        public void Initialize(object view, object viewModel)
        {
            if (view is ReactiveWindow<TViewModel> reactiveView && viewModel is TViewModel reactiveViewModel)
            {
                reactiveView.DataContext = reactiveViewModel;
                reactiveView.ViewModel = reactiveViewModel;
            }
        }
    }
}
