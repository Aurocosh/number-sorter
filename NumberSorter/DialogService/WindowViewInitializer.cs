using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
