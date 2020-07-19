using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Core.CustomGenerators;
using System.Collections.ObjectModel;
using System;
using NumberSorter.Core.CustomGenerators.Context;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.Interactions;
using Newtonsoft.Json;
using System.IO;
using NumberSorter.Domain.Serialization;
using NumberSorter.Domain.Container;

namespace NumberSorter.Domain.ViewModels
{
    public class GeneratorsDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly JsonFileSerializer _jsonFileSerializer;

        private readonly SourceList<CustomListGenerator> _listGenerators;
        private readonly ReadOnlyObservableCollection<ListGeneratorLineViewModel> _listGeneratorsViewModels;

        #endregion Fields

        #region Properties

        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public ListGeneratorLineViewModel SelectedListGenerator { get; set; }

        public UnsortedInput<int> InputNumbers { get; private set; }
        public ReadOnlyObservableCollection<ListGeneratorLineViewModel> ListGeneratorLineViewModels => _listGeneratorsViewModels;


        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AddNewGeneratorCommand { get; }
        public ReactiveCommand<Unit, bool> RemoveSelectedGeneratorCommand { get; }

        public ReactiveCommand<Unit, string> SerializeGeneratorCommand { get; }
        public ReactiveCommand<Unit, string> DeserializeGeneratorCommand { get; }

        public ReactiveCommand<Unit, Unit> EditGeneratorCommand { get; }
        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public GeneratorsDialogViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;
            _listGenerators = new SourceList<CustomListGenerator>();

            InputNumbers = new UnsortedInput<int>();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            _jsonFileSerializer = new JsonFileSerializer(jsonSerializerSettings);

            var canAcceptPredicate = this.WhenAnyValue(x => x.SelectedListGenerator).Select(x => x != null);

            AddNewGeneratorCommand = ReactiveCommand.Create(AddNewGenerator);
            RemoveSelectedGeneratorCommand = ReactiveCommand.CreateFromObservable(AskToRemoveSelectedGenerator);

            SerializeGeneratorCommand = ReactiveCommand.CreateFromObservable(FindFileToSave);
            DeserializeGeneratorCommand = ReactiveCommand.CreateFromObservable(FindFileToOpen);

            EditGeneratorCommand = ReactiveCommand.Create(EditGenerator);
            AcceptCommand = ReactiveCommand.Create(Accept, canAcceptPredicate);

            RemoveSelectedGeneratorCommand
                .Where(x => x)
                .Subscribe(_ => RemoveSelectedGenerator());
            SerializeGeneratorCommand
                .Subscribe(SerializeGenerator);
            DeserializeGeneratorCommand
                .Subscribe(DeserializeGenerator);

            _listGenerators
                .Connect()
                .ObserveOnDispatcher()
                .Transform(x => new ListGeneratorLineViewModel(x))
                .Bind(out _listGeneratorsViewModels)
                .DisposeMany()
                .Subscribe();

            _listGenerators.AddRange(LoadGenerators());
        }

        #endregion Constructors

        #region Command functions

        private void AddNewGenerator()
        {
            _listGenerators.Add(new CustomListGenerator());
        }

        private IObservable<bool> AskToRemoveSelectedGenerator()
        {
            var questionData = new YesNoQuestionData("Remove selected generator", "Are you sure you want to delete current generator? This operation cannot be reversed.");
            return DialogInteractions.AskYesNoQuestion.Handle(questionData);
        }

        private void RemoveSelectedGenerator()
        {
            if (SelectedListGenerator == null)
                return;

            var generator = SelectedListGenerator.ListGenerator;
            _listGenerators.Remove(generator);

            var filePath = GetGeneratorPath(generator);
            File.Delete(filePath);
        }

        private IObservable<string> FindFileToOpen()
        {
            return DialogInteractions.FindFileToOpenWithType.Handle("Json files (*.json)|*.json");
        }

        private IObservable<string> FindFileToSave()
        {
            return DialogInteractions.FindFileToSaveWithType.Handle("Json files (*.json)|*.json");
        }

        private void SerializeGenerator(string filePath)
        {
            if (filePath.Length == 0)
                return;
            if (SelectedListGenerator.ListGenerator == null)
                return;
            _jsonFileSerializer.SaveToJsonFile(filePath, SelectedListGenerator.ListGenerator);
        }

        private void DeserializeGenerator(string filePath)
        {
            var generator = _jsonFileSerializer.LoadFromJsonFile<CustomListGenerator>(filePath);
            if (generator != null)
                _listGenerators.Add(generator);
        }

        private void EditGenerator()
        {
            if (SelectedListGenerator == null)
                return;

            var generator = SelectedListGenerator.ListGenerator;
            var viewModel = new GeneratorEditDialogViewModel(_dialogService, generator);
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult == true)
            {
                var newGenerator = viewModel.GetCustomListGenerator();
                _listGenerators.Replace(generator, newGenerator);
                var filePath = GetGeneratorPath(newGenerator);
                _jsonFileSerializer.SaveToJsonFile(filePath, newGenerator);
            }
        }

        private void Accept()
        {
            var generator = SelectedListGenerator.ListGenerator;
            var context = new StandardConverterContext();
            var numbers = generator.GenerateList(context);
            InputNumbers = new UnsortedInput<int>(generator.Name, numbers);
            DialogResult = SelectedListGenerator != null;
        }

        #endregion Command functions

        #region Functions

        private IEnumerable<CustomListGenerator> LoadGenerators()
        {
            if (!Directory.Exists(FilePaths.GeneratorsFolder))
                return Enumerable.Empty<CustomListGenerator>();

            string[] filePaths = Directory.GetFiles(FilePaths.GeneratorsFolder);
            return filePaths.Select(x => _jsonFileSerializer.LoadFromJsonFile<CustomListGenerator>(x)).Where(x => x != null);
        }

        private static string GetGeneratorPath(CustomListGenerator listGenerator)
        {
            return Path.Combine(FilePaths.GeneratorsFolder, $"generator-{listGenerator.Id}.json");
        }

        #endregion
    }
}
