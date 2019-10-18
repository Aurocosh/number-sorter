using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using NumberSorter.DialogService;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.ViewModels;
using NumberSorter.Forms;
using NumberSorter.Interactions;
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
            dialogService.RegisterWindowType<SortTypeViewModel, SortTypeDialog>(new WindowViewInitializer<SortTypeViewModel>());
            dialogService.RegisterWindowType<GeneratorsDialogViewModel, GeneratorsDialog>(new WindowViewInitializer<GeneratorsDialogViewModel>());
            dialogService.RegisterWindowType<LogHistoryDialogViewModel, LogHistoryDialog>(new WindowViewInitializer<LogHistoryDialogViewModel>());
            dialogService.RegisterWindowType<NumberGeneratorsViewModel, NumberGeneratorsWindow>(new WindowViewInitializer<NumberGeneratorsViewModel>());
            dialogService.RegisterWindowType<GeneratorEditDialogViewModel, GeneratorEditDialog>(new WindowViewInitializer<GeneratorEditDialogViewModel>());
            dialogService.RegisterWindowType<PartialSortedGeneratorViewModel, PartialSortedGeneratorDialog>(new WindowViewInitializer<PartialSortedGeneratorViewModel>());

            WpfInteractions.RegisterInteractions();

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
