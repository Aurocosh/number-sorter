using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace NumberSorter.DialogService
{
    public class DialogService<TViewModel> : IDialogService<TViewModel> where TViewModel : class
    {
        private readonly Dictionary<Type, Type> _viewModelToViewMapping = new Dictionary<Type, Type>();
        private readonly Dictionary<TViewModel, Window> _openWindows = new Dictionary<TViewModel, Window>();
        private readonly Dictionary<Type, IViewInitializer> _viewInitializerMapping = new Dictionary<Type, IViewInitializer>();

        public void RegisterWindowType<TSpecificViewModel, TWindow>(IViewInitializer viewInitializer) where TWindow : Window
        {
            var viewType = typeof(TWindow);
            var viewModelType = typeof(TSpecificViewModel);
            if (viewModelType.IsInterface)
                throw new ArgumentException("Cannot register interfaces");
            if (_viewModelToViewMapping.ContainsKey(viewModelType))
                throw new InvalidOperationException($"Type {viewModelType.FullName} is already registered");
            _viewModelToViewMapping[viewModelType] = viewType;
            _viewInitializerMapping[viewModelType] = viewInitializer;
        }

        public void UnregisterWindowType()
        {
            var viewModelType = typeof(TViewModel);
            _viewModelToViewMapping.Remove(viewModelType);
        }

        public Window CreateWindowInstanceWithVM(TViewModel parentViewModel, TViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var viewModelType = viewModel.GetType();
            if (!_viewModelToViewMapping.TryGetValue(viewModelType, out Type viewType))
                throw new ArgumentException($"No registered window type for argument type {viewModel.GetType().FullName}");
            if (!_viewInitializerMapping.TryGetValue(viewModelType, out IViewInitializer viewInitializer))
                throw new ArgumentException($"No registered view initializer for argument type {viewModel.GetType().FullName}");

            var window = (Window)Activator.CreateInstance(viewType);
            viewInitializer.Initialize(window, viewModel);

            if (parentViewModel != null && _openWindows.TryGetValue(parentViewModel, out Window parentWindow))
            {
                window.Owner = parentWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            return window;
        }

        public void ShowPresentation(TViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (_openWindows.ContainsKey(viewModel))
                throw new InvalidOperationException("UI for this VM is already displayed");
            var window = CreateWindowInstanceWithVM(null, viewModel);
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

        public void ShowModalPresentation(TViewModel parentViewModel, TViewModel viewModel)
        {
            var window = CreateWindowInstanceWithVM(parentViewModel, viewModel);
            window.ShowDialog();
        }

        public async Task ShowModalPresentationAsync(TViewModel parentViewModel, TViewModel viewModel)
        {
            var window = CreateWindowInstanceWithVM(parentViewModel, viewModel);
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }
    }
}
