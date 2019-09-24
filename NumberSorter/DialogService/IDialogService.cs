using System.Threading.Tasks;
using System.Windows;

namespace NumberSorter.DialogService
{
    public interface IDialogService<TViewModel> where TViewModel : class
    {
        void ShowPresentation(TViewModel viewModel);
        void HidePresentation(TViewModel viewModel);
        void ShowModalPresentation(TViewModel parentViewModel, TViewModel viewModel);
        Task ShowModalPresentationAsync(TViewModel parentViewModel, TViewModel viewModel);
    }
}
