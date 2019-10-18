﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Text.RegularExpressions;
using NumberSorter.Domain.DialogService;
using NumberSorter.Core.Logic;
using NumberSorter.Core.Logic.Comparer;
using System.Diagnostics;
using NumberSorter.Domain.Interactions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Visualizers;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NumberSorter.Domain.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly JsonFileSerializer _jsonFileSerializer;

        #endregion Fields

        #region Properties

        public VisualizationViewModel VisualizationViewModel { get; }

        [Reactive] public string InputText { get; set; }
        [Reactive] public string OutputText { get; set; }
        [Reactive] public string InfoText { get; set; }
        [Reactive] public string ResultText { get; set; }

        [Reactive] public UnsortedInput<int> InputNumbers { get; set; }
        [Reactive] public SortLog<int> SortingLog { get; set; }


        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, string> LoadDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateRandomCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateCustomCommand { get; }
        public ReactiveCommand<Unit, List<int>> GeneratePartiallySortedCommand { get; }

        public ReactiveCommand<Unit, Unit> PerformSortCommand { get; }
        public ReactiveCommand<Unit, Unit> SortHistoryCommand { get; }

        #endregion Commands

        #region Constructors

        public MainWindowViewModel() : this(null) { }

        public MainWindowViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None
            };
            _jsonFileSerializer = new JsonFileSerializer(jsonSerializerSettings);

            VisualizationViewModel = new VisualizationViewModel(_dialogService);

            InputNumbers = new UnsortedInput<int>();
            SortingLog = new SortLog<int>();

            var canPerformSort = this.WhenAnyValue(x => x.InputNumbers)
                .Select(x => x.Count > 0);

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateRandomCommand = ReactiveCommand.CreateFromObservable(GenerateRandom);
            GenerateCustomCommand = ReactiveCommand.CreateFromObservable(GenerateCustom);
            GeneratePartiallySortedCommand = ReactiveCommand.CreateFromObservable(GeneratePartiallySorted);

            PerformSortCommand = ReactiveCommand.Create(SortData, canPerformSort);
            SortHistoryCommand = ReactiveCommand.Create(SortHistory);

            LoadDataCommand
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(LoadNumbersFromFile)
                .Subscribe(SetUnsortedInput);

            GenerateCustomCommand
                .Subscribe(SetUnsortedInput);
            GenerateRandomCommand
                .Subscribe(SetUnsortedInput);
            GeneratePartiallySortedCommand
                .Subscribe(SetUnsortedInput);

            this.WhenAnyValue(x => x.InputNumbers)
                .Subscribe(UpdateInputText);

            this.WhenAnyValue(x => x.SortingLog)
                .Do(x => VisualizationViewModel.SortingLog = x)
                .Subscribe(UpdateOutputText);
        }

        #endregion Constructors

        #region Command functions

        private IObservable<string> FindFileToLoad()
        {
            return DialogInteractions.FindFileToOpenWithType.Handle("Txt files (*.txt)|*.txt");
        }

        private List<int> LoadNumbersFromFile(string filepath)
        {
            var fileText = File.ReadAllText(filepath);
            fileText = Regex.Replace(fileText, @"[^\d\-]", " ");
            fileText = Regex.Replace(fileText, @"\-\s", " ");
            fileText = Regex.Replace(fileText, @"(\d+)\-(\d+)", "%1 -$2");
            fileText = Regex.Replace(fileText, @"\s+", " ");
            fileText = fileText.Trim();

            return fileText.Split(' ')
                .Select(x => int.Parse(x))
                .ToList();
        }

        private IObservable<List<int>> GenerateRandom()
        {
            var viewModel = new NumberGeneratorsViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.Numbers);
        }

        private IObservable<List<int>> GenerateCustom()
        {
            var viewModel = new GeneratorsDialogViewModel(_dialogService);
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.Numbers);
        }

        private IObservable<List<int>> GeneratePartiallySorted()
        {
            var viewModel = new PartialSortedGeneratorViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.Numbers);
        }

        private void SortData()
        {
            var viewModel = new SortTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedSortType == null)
                return;

            var accessTrackingList = new LoggingList<int>(InputNumbers.Values, new IntComparer());

            var algorhythmType = viewModel.SelectedSortType.AlgorhythmType;
            var algorhythm = AlgorhythmFactory.GetAlgorhythm(algorhythmType, accessTrackingList);

            GC.Collect();
            var stopwatch = Stopwatch.StartNew();

            algorhythm.Sort(accessTrackingList);

            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            var algorhythmName = AlgorhythmNamer.GetName(algorhythmType);
            SortingLog = accessTrackingList.GetSortLog(InputNumbers.Id, algorhythmName, elapsedTime);
            SaveLogSummary(SortingLog);
        }

        private void SortHistory()
        {
            var viewModel = new LogHistoryDialogViewModel(_dialogService);
            _dialogService.ShowModalPresentation(this, viewModel);
            if (viewModel.DialogResult == true)
                InputNumbers = new UnsortedInput<int>(viewModel.InputNumbers);
        }

        #endregion Command functions

        private void SaveLogSummary(SortLog<int> sortLog)
        {
            var inputId = sortLog.Summary.StartId.ToString();
            var resultId = sortLog.Summary.FinishId.ToString();
            var summaryFilePath = Path.Combine(FilePaths.LogFolder, inputId, $"{resultId}.json");
            _jsonFileSerializer.SaveToJsonFile(summaryFilePath, sortLog.Summary);

            var inputFilePath = Path.Combine(FilePaths.InputsListsFolder, $"{inputId}.json");
            _jsonFileSerializer.SaveToJsonFile(inputFilePath, sortLog.InputState.State);
        }

        private void SetUnsortedInput(IEnumerable<int> input)
        {
            InputNumbers = new UnsortedInput<int>(input);
        }

        private void UpdateInputText(UnsortedInput<int> input)
        {
            InputText = string.Join(", ", input.Values.Take(1000).Select(x => x.ToString()));
            InfoText = $"Input number count: {input.Count}";
        }

        private void UpdateOutputText(SortLog<int> sortLog)
        {
            var summary = sortLog.Summary;
            OutputText = string.Join(", ", sortLog.FinalState.State.Take(1000).Select(x => x.ToString()));
            ResultText = summary.FullySorted ? $"Write count: {summary.TotalWriteCount}\nRead count: {summary.TotalReadCount}\nTime: {summary.ElapsedTime}" : "";
        }
    }
}
