﻿using System.Reflection;
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
            dialogService.RegisterWindowType<ColorSetDialogViewModel, ColorSetDialog>(new WindowViewInitializer<ColorSetDialogViewModel>());
            dialogService.RegisterWindowType<GeneratorsDialogViewModel, GeneratorsDialog>(new WindowViewInitializer<GeneratorsDialogViewModel>());
            dialogService.RegisterWindowType<LogHistoryDialogViewModel, LogHistoryDialog>(new WindowViewInitializer<LogHistoryDialogViewModel>());
            dialogService.RegisterWindowType<NumberGeneratorsViewModel, NumberGeneratorsWindow>(new WindowViewInitializer<NumberGeneratorsViewModel>());
            dialogService.RegisterWindowType<GeneratorEditDialogViewModel, GeneratorEditDialog>(new WindowViewInitializer<GeneratorEditDialogViewModel>());
            dialogService.RegisterWindowType<ColorSetSelectDialogViewModel, ColorSetSelectDialog>(new WindowViewInitializer<ColorSetSelectDialogViewModel>());
            dialogService.RegisterWindowType<ComparassionSortTypeViewModel, ComparassionSortTypeDialog>(new WindowViewInitializer<ComparassionSortTypeViewModel>());
            dialogService.RegisterWindowType<DistributionSortTypeViewModel, DistributionSortTypeDialog>(new WindowViewInitializer<DistributionSortTypeViewModel>());
            dialogService.RegisterWindowType<PartialSortedGeneratorViewModel, PartialSortedGeneratorDialog>(new WindowViewInitializer<PartialSortedGeneratorViewModel>());

            dialogService.RegisterWindowType<MidiAudiolizerSettingsDialogViewModel, MidiAudiolizerSettingsDialog>(new WindowViewInitializer<MidiAudiolizerSettingsDialogViewModel>());

            dialogService.RegisterWindowType<AudiolizerTypeDialogViewModel, AudiolizerTypeDialog>(new WindowViewInitializer<AudiolizerTypeDialogViewModel>());
            dialogService.RegisterWindowType<VisualizationTypeViewModel, VisualizationTypeDialog>(new WindowViewInitializer<VisualizationTypeViewModel>());

            dialogService.RegisterWindowType<TimSortDialogViewModel, TimSortDialog>(new WindowViewInitializer<TimSortDialogViewModel>());
            dialogService.RegisterWindowType<JHeapSortDialogViewModel, JHeapSortDialog>(new WindowViewInitializer<JHeapSortDialogViewModel>());
            dialogService.RegisterWindowType<QuickSortDialogViewModel, QuickSortDialog>(new WindowViewInitializer<QuickSortDialogViewModel>());
            dialogService.RegisterWindowType<ShellSortDialogViewModel, ShellSortDialog>(new WindowViewInitializer<ShellSortDialogViewModel>());
            dialogService.RegisterWindowType<KWayMergeSortDialogViewModel, KWayMergeSortDialog>(new WindowViewInitializer<KWayMergeSortDialogViewModel>());
            dialogService.RegisterWindowType<MultiMergeSortDialogViewModel, MultiMergeSortDialog>(new WindowViewInitializer<MultiMergeSortDialogViewModel>());
            dialogService.RegisterWindowType<IntervalMergeSortDialogViewModel, IntervalMergeSortDialog>(new WindowViewInitializer<IntervalMergeSortDialogViewModel>());

            dialogService.RegisterWindowType<BiasValueDialogViewModel, BiasValueDialog>(new WindowViewInitializer<BiasValueDialogViewModel>());
            dialogService.RegisterWindowType<GroupingRunLocatorDialogViewModel, GroupingRunLocatorDialog>(new WindowViewInitializer<GroupingRunLocatorDialogViewModel>());

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
