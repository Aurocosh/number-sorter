using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Core.Logic;
using System;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Visualizers;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.AppColors;
using System.IO;
using NumberSorter.Domain.Serialization;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace NumberSorter.Domain.ViewModels
{
    public class ColorSetSelectDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly JsonFileSerializer _jsonFileSerializer;
        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly SourceList<ColorSet> _colorSets = new SourceList<ColorSet>();
        private readonly ReadOnlyObservableCollection<ColorSetLineViewModel> _colorSetViewModels;

        #endregion Fields

        #region Properties
        [Reactive] public ColorSet ColorSet { get; private set; }

        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public ColorSetLineViewModel SelectedColorSet { get; set; }
        public IEnumerable<ColorSetLineViewModel> ColorSets => _colorSetViewModels;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AddNewCommand { get; }
        public ReactiveCommand<Unit, Unit> EditSelectedCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteSelectedCommand { get; }

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public ColorSetSelectDialogViewModel() : this(null) { }

        public ColorSetSelectDialogViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None
            };
            _jsonFileSerializer = new JsonFileSerializer(jsonSerializerSettings);

            ColorSet = new ColorSet();

            var selectedNotNull = this.WhenAnyValue(x => x.SelectedColorSet).Select(x => x != null);

            AddNewCommand = ReactiveCommand.Create(AddNew);
            EditSelectedCommand = ReactiveCommand.Create(EditSelected, selectedNotNull);
            DeleteSelectedCommand = ReactiveCommand.Create(DeleteSelected, selectedNotNull);

            AcceptCommand = ReactiveCommand.Create(Accept, selectedNotNull);

            _colorSets.Connect()
                .Transform(x => new ColorSetLineViewModel(x))
                .ObserveOnDispatcher()
                .Bind(out _colorSetViewModels)
                .DisposeMany()
                .Subscribe();

            _colorSets.AddRange(LoadColorSets());
        }

        #endregion Constructors

        #region Command functions

        private void AddNew()
        {
            var colorSet = new ColorSet();
            var viewModel = new ColorSetDialogViewModel(colorSet);
            _dialogService.ShowModalPresentation(this, viewModel);
            if (viewModel.DialogResult == true)
            {
                var newColorSet = viewModel.ColorSet;
                _colorSets.Add(newColorSet);
                var filePath = GetColorSetPath(newColorSet);
                _jsonFileSerializer.SaveToJsonFile(filePath, newColorSet);
            }
        }

        private void EditSelected()
        {
            var colorSet = SelectedColorSet.ColorSet;
            var viewModel = new ColorSetDialogViewModel(colorSet);
            _dialogService.ShowModalPresentation(this, viewModel);
            if (viewModel.DialogResult == true)
            {
                var newColorSet = viewModel.ColorSet;
                _colorSets.Replace(colorSet, newColorSet);
                var filePath = GetColorSetPath(newColorSet);
                _jsonFileSerializer.SaveToJsonFile(filePath, newColorSet);
            }
        }

        private void DeleteSelected()
        {
            var colorSet = SelectedColorSet.ColorSet;
            _colorSets.Remove(colorSet);

            var filePath = GetColorSetPath(colorSet);
            File.Delete(filePath);
        }

        private void Accept()
        {
            ColorSet = SelectedColorSet.ColorSet;
            DialogResult = SelectedColorSet != null;
        }

        #endregion Command functions

        #region Functions

        private IEnumerable<ColorSet> LoadColorSets()
        {
            if (!Directory.Exists(FilePaths.ColorSetsFolder))
                return Enumerable.Empty<ColorSet>();

            string[] filePaths = Directory.GetFiles(FilePaths.ColorSetsFolder);
            return filePaths.Select(x => _jsonFileSerializer.LoadFromJsonFile<ColorSet>(x)).Where(x => x != null);
        }

        private static string GetColorSetPath(ColorSet colorSet)
        {
            return Path.Combine(FilePaths.ColorSetsFolder, $"color-set-{colorSet.Id}.json");
        }

        #endregion
    }
}
