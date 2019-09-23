using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace NumberSorter.DialogService
{
    public class DialogService<TViewModel> : IDialogService<TViewModel> where TViewModel : class
    {
        private readonly Dictionary<TViewModel, Window> _openWindows = new Dictionary<TViewModel, Window>();
        private readonly Dictionary<Type, Func<TViewModel, Window>> _viewFactoryMapping = new Dictionary<Type, Func<TViewModel, Window>>();

        public void RegisterWindowType<TSpecificViewModel, TWindow>(Func<TViewModel, TWindow> viewFactory) where TWindow : Window
        {
            var viewModelType = typeof(TSpecificViewModel);
            if (viewModelType.IsInterface)
                throw new ArgumentException("Cannot register interfaces");
            if (_viewFactoryMapping.ContainsKey(viewModelType))
                throw new InvalidOperationException($"Type {viewModelType.FullName} is already registered");
            _viewFactoryMapping[viewModelType] = viewFactory;
        }

        public void UnregisterWindowType()
        {
            var viewModelType = typeof(TViewModel);
            if (viewModelType.IsInterface)
                throw new ArgumentException("Cannot register interfaces");
            if (!_viewFactoryMapping.ContainsKey(viewModelType))
                throw new InvalidOperationException($"Type {viewModelType.FullName} is not registered");
            _viewFactoryMapping.Remove(viewModelType);
        }

        public Window CreateWindowInstanceWithVM(TViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var viewModelType = viewModel.GetType();
            if (!_viewFactoryMapping.TryGetValue(viewModelType, out Func<TViewModel, Window> viewFactory))
                throw new ArgumentException($"No registered window type for argument type {viewModel.GetType().FullName}");

            return viewFactory.Invoke(viewModel);
        }

        public void ShowPresentation(TViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (_openWindows.ContainsKey(viewModel))
                throw new InvalidOperationException("UI for this VM is already displayed");
            var window = CreateWindowInstanceWithVM(viewModel);
            window.Show();
            _openWindows[viewModel] = window;
        }

        public void HidePresentation(TViewModel viewModel)
        {
            if (!_openWindows.TryGetValue(viewModel, out Window window))
                throw new InvalidOperationException("UI for this VM is not displayed");
            window.Close();
            _openWindows.Remove(viewModel);
        }

        public async Task ShowModalPresentation(TViewModel parentViewModel, TViewModel viewModel)
        {
            var window = CreateWindowInstanceWithVM(viewModel);
            if (parentViewModel != null && _openWindows.TryGetValue(parentViewModel, out Window parentWindow))
            {
                window.Owner = parentWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }
    }
}
