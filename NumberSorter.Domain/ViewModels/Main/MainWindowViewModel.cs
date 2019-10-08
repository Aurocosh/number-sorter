using System;
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

namespace NumberSorter.Domain.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;

        #endregion Fields

        #region Properties

        public VisualizationViewModel VisualizationViewModel { get; }

        [Reactive] public string InputText { get; set; }
        [Reactive] public string OutputText { get; set; }
        [Reactive] public string InfoText { get; set; }
        [Reactive] public string ResultText { get; set; }

        [Reactive] public List<int> InputNumbers { get; set; }
        [Reactive] public SortLog<int> SortingLog { get; set; }


        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, string> LoadDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateRandomCommand { get; }
        public ReactiveCommand<Unit, List<int>> GeneratePartiallySortedCommand { get; }
        public ReactiveCommand<Unit, Unit> PerformSortCommand { get; }

        #endregion Commands


        #region Constructors

        public MainWindowViewModel() : this(null) { }

        public MainWindowViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            VisualizationViewModel = new VisualizationViewModel(_dialogService);

            InputNumbers = new List<int>();
            SortingLog = new SortLog<int>();

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateRandomCommand = ReactiveCommand.CreateFromObservable(GenerateRandom);
            GeneratePartiallySortedCommand = ReactiveCommand.CreateFromObservable(GeneratePartiallySorted);
            PerformSortCommand = ReactiveCommand.Create(SortData);

            LoadDataCommand
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(LoadNumbersFromFile)
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            GenerateRandomCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);
            GeneratePartiallySortedCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            this.WhenAnyValue(x => x.InputNumbers)
                .Subscribe(UpdateInputText);

            this.WhenAnyValue(x => x.SortingLog)
                .Do(x => VisualizationViewModel.SortingLog = x)
                .Subscribe(UpdateOutputText);
        }

        #endregion Constructors

        #region Command functions

        private IObservable<String> FindFileToLoad()
        {
            return DialogInteractions.FindFileWithType.Handle("Txt files (*.txt)|*.txt");
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
            var viewModel = new NumberGeneratorViewModel();
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

            var accessTrackingList = new LoggingList<int>(InputNumbers, new IntComparer());

            var algorhythmType = viewModel.SelectedSortType.AlgorhythmType;
            var algorhythm = AlgorhythmFactory.GetAlgorhythm<LogValue<int>>(algorhythmType, accessTrackingList);

            GC.Collect();
            var stopwatch = Stopwatch.StartNew();

            algorhythm.Sort(accessTrackingList);

            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            SortingLog = accessTrackingList.GetSortLog(elapsedTime);
        }

        #endregion Command functions

        #region Command predicates

        private bool CanAnalizeSeries => true;

        #endregion Command predicates

        private void UpdateInputText(List<int> values)
        {
            InputText = string.Join(", ", values.Select(x => x.ToString()));
            InfoText = $"Input number count: {values.Count}";
        }

        private void UpdateOutputText(SortLog<int> sortLog)
        {
            OutputText = string.Join(", ", sortLog.FinalState.Select(x => x.ToString()));
            ResultText = sortLog.FullySorted ? $"Write count: {sortLog.TotalWriteCount}\nRead count: {sortLog.TotalReadCount}\nTime: {sortLog.ElapsedTime}" : "";
        }
    }
}
