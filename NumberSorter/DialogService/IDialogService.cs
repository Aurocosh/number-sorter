using System.Threading.Tasks;
using System.Windows;

namespace NumberSorter.DialogService
{
    public interface IDialogService<TViewModel> where TViewModel : class
    {
        void ShowPresentation(TViewModel viewModel);
        void HidePresentation(TViewModel viewModel);
        Task ShowModalPresentation(TViewModel parentViewModel, TViewModel viewModel);
    }
}
