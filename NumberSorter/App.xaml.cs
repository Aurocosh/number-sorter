using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using NumberSorter.DialogService;
using NumberSorter.Forms;
using NumberSorter.Interactions;
using NumberSorter.ViewModels;
using ReactiveUI;
using Splat;

namespace NumberSorter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IDialogService<ReactiveObject> _dialogService;

        public App()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            var dialogService = new DialogService<ReactiveObject>();
            dialogService.RegisterWindowType<MainWindowViewModel, MainWindow>(new WindowViewInitializer<MainWindowViewModel>());
            dialogService.RegisterWindowType<NumberGeneratorViewModel, NumberGeneratorWindow>(new WindowViewInitializer<NumberGeneratorViewModel>());
            dialogService.RegisterWindowType<SortTypeViewModel, SortTypeDialog>(new WindowViewInitializer<SortTypeViewModel>());

            _dialogService = dialogService;
        }


        protected override async void OnStartup(StartupEventArgs eventArgs)
        {
            base.OnStartup(eventArgs);
            var mainWindowViewModel = new MainWindowViewModel(_dialogService);
            await _dialogService.ShowModalPresentationAsync(null, mainWindowViewModel).ConfigureAwait(false);
        }
    }
}
