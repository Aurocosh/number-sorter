using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using NumberSorter.Forms;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.Windows.Forms;
using NumberSorter.Interactions;
using System.IO;
using System.Text.RegularExpressions;
using NumberSorter.DialogService;
using NumberSorter.Logic;
using NumberSorter.Algorhythm.Container;
using NumberSorter.Logic.Comparer;

namespace NumberSorter.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;

        #endregion Fields

        #region Properties

        [Reactive] public string InputText { get; set; }
        [Reactive] public string OutputText { get; set; }
        [Reactive] public IList<int> InputNumbers { get; set; }
        [Reactive] public IList<int> OutputNumbers { get; set; }

        #endregion Properties


        #region Commands

        public ReactiveCommand<Unit, String> LoadDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> PerformSortCommand { get; }

        #endregion Commands


        #region Constructors

        public MainWindowViewModel() : this(null) { }

        public MainWindowViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            InputNumbers = new List<int>();
            OutputNumbers = new List<int>();

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateDataCommand = ReactiveCommand.CreateFromObservable(GenerateData);
            PerformSortCommand = ReactiveCommand.CreateFromObservable(SortData);

            LoadDataCommand
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(LoadNumbersFromFile)
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            GenerateDataCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            PerformSortCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => OutputNumbers = x);

            this.WhenAnyValue(x => x.InputNumbers)
                .Subscribe(UpdateInputText);

            this.WhenAnyValue(x => x.OutputNumbers)
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
            fileText = Regex.Replace(fileText, @"[^\d\-,.]", " ");
            fileText = Regex.Replace(fileText, @"\-\s", " ");
            fileText = Regex.Replace(fileText, @"(\d+)\-(\d+)", "%1 $2");
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

        private IObservable<List<int>> SortData()
        {
            var viewModel = new SortTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult == false || viewModel.SelectedSortType == null)
                return Observable.Return(new List<int>());

            var algorhythmType = viewModel.SelectedSortType.AlgorhythmType;
            var factory = new AlgorhythmFactory();
            var algorhythm = factory.GetAlgorhythm(algorhythmType);

            var sortingContainer = new AccessTrackingList<int>(InputNumbers);
            algorhythm.Sort(sortingContainer, new IntComparer());
            var result = sortingContainer.ToList();

            return Observable.Return(result);
        }

        #endregion Command functions

        #region Command predicates

        private bool CanAnalizeSeries => true;

        #endregion Command predicates

        private void UpdateInputText(IEnumerable<int> values) => InputText = string.Join(", ", values.Select(x => x.ToString()));
        private void UpdateOutputText(IEnumerable<int> values) => OutputText = String.Join(", ", values.Select(x => x.ToString()));
    }
}
