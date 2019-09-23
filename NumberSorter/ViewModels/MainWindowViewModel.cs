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

        public List<int> InputNumbers { [ObservableAsProperty]get; }

        #endregion Properties


        #region Commands

        public ReactiveCommand<Unit, String> LoadDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateDataCommand { get; }
        public ReactiveCommand<Unit, Unit> PerformSortCommand { get; }

        #endregion Commands


        #region Constructors

        public MainWindowViewModel() : this(null) { }

        public MainWindowViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateDataCommand = ReactiveCommand.CreateFromObservable(GenerateData);
            PerformSortCommand = ReactiveCommand.Create(SortData);

            GenerateDataCommand
                .Where(x => x?.Count > 0)
                .ToPropertyEx(this, x => x.InputNumbers);

            LoadDataCommand.
                Where(x => !string.IsNullOrEmpty(x))
                .Select(LoadNumbersFromFile)
                .Where(x => x.Count > 0)
                .ToPropertyEx(this, x => x.InputNumbers);

            this.WhenAnyValue(x => x.InputNumbers)
                .Subscribe(UpdateInputText);
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

        private void SortData()
        {
        }

        #endregion Command functions

        #region Command predicates

        private bool CanAnalizeSeries => true;

        #endregion Command predicates

        private void UpdateInputText(List<int> values)
        {
            if (values != null)
                InputText = String.Join(", ", values.Select(x => x.ToString()));
        }
    }
}
