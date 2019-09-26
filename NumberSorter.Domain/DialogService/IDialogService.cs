using System.Threading.Tasks;

namespace NumberSorter.Domain.DialogService
{
    public interface IDialogService<TViewModel> where TViewModel : class
    {
        void ShowPresentation(TViewModel viewModel);
        void HidePresentation(TViewModel viewModel);
        bool ShowModalPresentation(TViewModel parentViewModel, TViewModel viewModel);
        Task ShowModalPresentationAsync(TViewModel parentViewModel, TViewModel viewModel);
    }
}
