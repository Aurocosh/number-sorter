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
            var dialogService = new DialogService<ReactiveObject>();
            dialogService.RegisterWindowType<MainWindowViewModel, MainWindow>(x => new MainWindow(x as MainWindowViewModel));
            dialogService.RegisterWindowType<NumberGeneratorViewModel, NumberGeneratorWindow>(x => new NumberGeneratorWindow(x as NumberGeneratorViewModel));

            _dialogService = dialogService;
        }


        protected override async void OnStartup(StartupEventArgs eventArgs)
        {
            base.OnStartup(eventArgs);

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            var mainWindowViewModel = new MainWindowViewModel(_dialogService);
            await _dialogService.ShowModalPresentation(null, mainWindowViewModel).ConfigureAwait(false);
        }
    }
}
