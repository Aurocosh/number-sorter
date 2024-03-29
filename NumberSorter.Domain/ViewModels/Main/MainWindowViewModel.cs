﻿using System;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Text.RegularExpressions;
using NumberSorter.Domain.DialogService;
using NumberSorter.Core.Logic.Comparer;
using System.Diagnostics;
using NumberSorter.Domain.Interactions;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Serialization;
using Newtonsoft.Json;
using NumberSorter.Domain.Logic;
using NumberSorter.Domain.AppColors;

namespace NumberSorter.Domain.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Fields

        private readonly JsonFileSerializer _jsonFileSerializer;
        private readonly IDialogService<ReactiveObject> _dialogService;

        #endregion Fields

        #region Properties

        public VisualizationViewModel VisualizationViewModel { get; }

        [Reactive] public Thickness Margin { get; set; }
        [Reactive] public bool IsFullscreen { get; set; }
        [Reactive] public bool ShowControls { get; set; }

        [Reactive] public string InputText { get; set; }
        [Reactive] public string OutputText { get; set; }
        [Reactive] public string InfoText { get; set; }
        [Reactive] public string ResultText { get; set; }

        [Reactive] public bool Topmost { get; set; }
        [Reactive] public ResizeMode ResizeMode { get; set; }
        [Reactive] public WindowState WindowState { get; set; }
        [Reactive] public WindowStyle WindowStyle { get; set; }

        [Reactive] public UnsortedInput<int> InputNumbers { get; set; }
        [Reactive] public SortLog<int> SortingLog { get; set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, string> LoadDataCommand { get; }
        public ReactiveCommand<Unit, UnsortedInput<int>> GenerateRandomCommand { get; }
        public ReactiveCommand<Unit, UnsortedInput<int>> GenerateCustomCommand { get; }
        public ReactiveCommand<Unit, UnsortedInput<int>> GeneratePartiallySortedCommand { get; }

        public ReactiveCommand<Unit, Unit> ToggleControlsCommand { get; }
        public ReactiveCommand<KeyEventArgs, Unit> KeyPressCommand { get; }
        public ReactiveCommand<MouseButtonEventArgs, Unit> MouseButtonCommand { get; }

        public ReactiveCommand<Unit, Unit> PerformComparassionSortCommand { get; }
        public ReactiveCommand<Unit, Unit> PerformDistributionSortCommand { get; }
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

            var activeColorSetPath = Path.Combine(FilePaths.AppDataFolder, "ActiveColorSet.json");
            if (File.Exists(activeColorSetPath))
                VisualizationViewModel.ColorSet = _jsonFileSerializer.LoadFromJsonFile<ColorSet>(activeColorSetPath);

            Topmost = false;
            ResizeMode = ResizeMode.CanResize;
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Maximized;

            Margin = new Thickness(5);

            ShowControls = true;
            IsFullscreen = false;

            InputNumbers = new UnsortedInput<int>();
            SortingLog = new SortLog<int>();

            var canPerformSort = this.WhenAnyValue(x => x.InputNumbers)
                .Select(x => x.Count > 0);

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateRandomCommand = ReactiveCommand.CreateFromObservable(GenerateRandom);
            GenerateCustomCommand = ReactiveCommand.CreateFromObservable(GenerateCustom);
            GeneratePartiallySortedCommand = ReactiveCommand.CreateFromObservable(GeneratePartiallySorted);

            ToggleControlsCommand = ReactiveCommand.Create(ToggleControlPanel);
            KeyPressCommand = ReactiveCommand.Create<KeyEventArgs>(KeyPress);
            MouseButtonCommand = ReactiveCommand.Create<MouseButtonEventArgs>(MouseButtonPressed);

            PerformComparassionSortCommand = ReactiveCommand.Create(PerformComparassionSort, canPerformSort);
            PerformDistributionSortCommand = ReactiveCommand.Create(PerformDistributionSort, canPerformSort);
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

        private UnsortedInput<int> LoadNumbersFromFile(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var fileText = File.ReadAllText(filePath);
            fileText = Regex.Replace(fileText, @"[^\d\-]", " ");
            fileText = Regex.Replace(fileText, @"\-\s", " ");
            fileText = Regex.Replace(fileText, @"(\d+)\-(\d+)", "%1 -$2");
            fileText = Regex.Replace(fileText, @"\s+", " ");
            fileText = fileText.Trim();

            var numbers = fileText.Split(' ')
                .Select(x => int.Parse(x))
                .ToList();

            return new UnsortedInput<int>(fileName, numbers);
        }

        private IObservable<UnsortedInput<int>> GenerateRandom()
        {
            var viewModel = new NumberGeneratorsViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.InputNumbers);
        }

        private IObservable<UnsortedInput<int>> GenerateCustom()
        {
            var viewModel = new GeneratorsDialogViewModel(_dialogService);
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.InputNumbers);
        }

        private IObservable<UnsortedInput<int>> GeneratePartiallySorted()
        {
            var viewModel = new PartialSortedGeneratorViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.InputNumbers);
        }

        private void KeyPress(KeyEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Key.F:
                    ToggleFullscreen();
                    break;
                case Key.V:
                    ToggleControlPanel();
                    break;
                case Key.Space:
                    VisualizationViewModel.PlayOrPauseAnimation();
                    break;
            }
        }

        private void MouseButtonPressed(MouseButtonEventArgs eventArgs)
        {
            switch (eventArgs.ChangedButton)
            {
                case MouseButton.Left:
                    VisualizationViewModel.PlayOrPauseAnimation();
                    break;
                case MouseButton.Right:
                    ToggleControlPanel();
                    break;
                case MouseButton.Middle:
                    ToggleFullscreen();
                    break;
            }
        }

        private void ToggleFullscreen()
        {
            IsFullscreen = !IsFullscreen;
            ShowControls = !IsFullscreen;
            if (IsFullscreen)
            {
                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
                WindowState = WindowState.Normal;
                WindowState = WindowState.Maximized;
                Topmost = false;
                Margin = new Thickness(0);
            }
            else
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                ResizeMode = ResizeMode.CanResize;
                WindowState = WindowState.Normal;
                Topmost = false;
                Margin = new Thickness(5);
            }
        }

        private void ToggleControlPanel()
        {
            ShowControls = !ShowControls;
        }

        private void PerformComparassionSort()
        {
            var viewModel = new ComparassionSortTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedSortType == null)
                return;

            var accessTrackingList = new ComparerLoggingList<int>(InputNumbers.Values, new IntComparer());

            var algorhythmType = viewModel.SelectedSortType.Type;
            var algorhythm = ComparassionAlgorhythmFactory.GetAlgorhythm(algorhythmType, this, _dialogService);
            if (algorhythm == null)
                return;

            GC.Collect();
            var stopwatch = Stopwatch.StartNew();

            algorhythm.Sort(accessTrackingList, accessTrackingList);

            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            var algorhythmName = ComparassionAlgorhythmNamer.GetName(algorhythmType);
            SortingLog = accessTrackingList.GetSortLog(InputNumbers.Name, InputNumbers.Id, algorhythmName, elapsedTime);
            SaveLogSummary(SortingLog);
        }

        private void PerformDistributionSort()
        {
            var viewModel = new DistributionSortTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedSortType == null)
                return;

            var algorhythmType = viewModel.SelectedSortType.AlgorhythmType;
            var algorhythm = DistributionAlgorhythmFactory.GetAlgorhythm(algorhythmType, _dialogService);
            if (algorhythm == null)
                return;

            var accessTrackingList = new AccessLoggingList<int>(InputNumbers.Values, new IntComparer());

            GC.Collect();
            var stopwatch = Stopwatch.StartNew();

            algorhythm.Sort(accessTrackingList);

            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            var algorhythmName = DistributionAlgorhythmNamer.GetName(algorhythmType);
            SortingLog = accessTrackingList.GetSortLog(InputNumbers.Name, InputNumbers.Id, algorhythmName, elapsedTime);
            SaveLogSummary(SortingLog);
        }

        private void SortHistory()
        {
            var viewModel = new LogHistoryDialogViewModel(_dialogService);
            _dialogService.ShowModalPresentation(this, viewModel);
            if (viewModel.DialogResult == true)
                InputNumbers = viewModel.InputNumbers;
        }

        private void Generate()
        {

        }

        #endregion Command functions

        private void SetUnsortedInput(UnsortedInput<int> input)
        {
            InputNumbers = input;
            SortingLog = new SortLog<int>(input.Name, input.Id, input.Values, input.Values);
        }

        private void SaveLogSummary(SortLog<int> sortLog)
        {
            var inputId = sortLog.Summary.InputId.ToString();
            var resultId = sortLog.Summary.FinishId.ToString();
            var summaryFilePath = Path.Combine(FilePaths.LogFolder, inputId, $"{resultId}.json");
            _jsonFileSerializer.SaveToJsonFile(summaryFilePath, sortLog.Summary);

            var inputFilePath = Path.Combine(FilePaths.InputsListsFolder, $"{inputId}.json");
            _jsonFileSerializer.SaveToJsonFile(inputFilePath, InputNumbers);
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
