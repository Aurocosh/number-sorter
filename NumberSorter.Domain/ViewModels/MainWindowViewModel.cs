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
using NumberSorter.Domain.Logic;
using NumberSorter.Domain.Algorhythm.Container;
using NumberSorter.Domain.Logic.Comparer;
using System.Diagnostics;
using NumberSorter.Domain.Interactions;

namespace NumberSorter.Domain.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;

        #endregion Fields

        #region Properties

        [Reactive] public string InputText { get; set; }
        [Reactive] public string OutputText { get; set; }
        [Reactive] public string InfoText { get; set; }
        [Reactive] public string ResultText { get; set; }

        [Reactive] public List<int> InputNumbers { get; set; }
        [Reactive] public SortingResult<int> SortingResult { get; set; }

        #endregion Properties


        #region Commands

        public ReactiveCommand<Unit, string> LoadDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateDataCommand { get; }
        public ReactiveCommand<Unit, Unit> PerformSortCommand { get; }

        #endregion Commands


        #region Constructors

        public MainWindowViewModel() : this(null) { }

        public MainWindowViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            InputNumbers = new List<int>();
            SortingResult = new SortingResult<int>();

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateDataCommand = ReactiveCommand.CreateFromObservable(GenerateData);
            PerformSortCommand = ReactiveCommand.Create(SortData);

            LoadDataCommand
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(LoadNumbersFromFile)
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            GenerateDataCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            this.WhenAnyValue(x => x.InputNumbers)
                .Subscribe(UpdateInputText);

            this.WhenAnyValue(x => x.SortingResult)
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

        private IObservable<List<int>> GenerateData()
        {
            var viewModel = new NumberGeneratorViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.Numbers);
        }

        private void SortData()
        {
            var viewModel = new SortTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedSortType == null)
                return;

            var algorhythmType = viewModel.SelectedSortType.AlgorhythmType;
            var algorhythm = AlgorhythmFactory.GetAlgorhythm(algorhythmType, new IntComparer());
            var accessTrackingList = new AccessTrackingList<int>(InputNumbers);

            GC.Collect();
            var stopwatch = Stopwatch.StartNew();

            algorhythm.Sort(accessTrackingList);

            stopwatch.Stop();

            var sortedList = accessTrackingList.ToList();

            int writeCount = accessTrackingList.WriteCount;
            int readCount = accessTrackingList.ReadCount;

            SortingResult = new SortingResult<int>(writeCount, readCount, stopwatch.ElapsedMilliseconds, sortedList);
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

        private void UpdateOutputText(SortingResult<int> result)
        {
            OutputText = string.Join(", ", result.SortedList.Select(x => x.ToString()));
            ResultText = result.Valid ? $"Write count: {result.WriteCount}\nRead count: {result.ReadCount}\nTime: {result.TimeSpent}" : "";
        }
    }
}
