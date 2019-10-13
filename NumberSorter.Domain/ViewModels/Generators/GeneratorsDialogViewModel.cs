using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Core.Logic;
using NumberSorter.Core.CustomGenerators;
using System.Collections.ObjectModel;
using System;
using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.CustomGenerators.Context;
using NumberSorter.Core.CustomGenerators.Processors.Generators;
using NumberSorter.Domain.DialogService;

namespace NumberSorter.Domain.ViewModels
{
    public class GeneratorsDialogViewModel : ReactiveObject
    {
        #region Fields

        private List<int> _numbers = new List<int>();
        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly SourceList<CustomListGenerator> _listGenerators = new SourceList<CustomListGenerator>();
        private readonly ReadOnlyObservableCollection<ListGeneratorLineViewModel> _listGeneratorsViewModels;
        private readonly IConverterContext _converterContext = new StandardConverterContext();

        #endregion Fields

        #region Properties

        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public ListGeneratorLineViewModel SelectedListGenerator { get; set; }

        public List<int> Numbers => new List<int>(_numbers);
        public ReadOnlyObservableCollection<ListGeneratorLineViewModel> ListGeneratorLineViewModels => _listGeneratorsViewModels;


        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AddNewGeneratorCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearAllGeneratorsCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelectedGeneratorCommand { get; }

        public ReactiveCommand<Unit, Unit> EditGeneratorCommand { get; }
        public ReactiveCommand<Unit, Unit> GenerateCommand { get; }
        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public GeneratorsDialogViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            AddNewGeneratorCommand = ReactiveCommand.Create(AddNewGenerator);
            ClearAllGeneratorsCommand = ReactiveCommand.Create(ClearAllGenerators);
            RemoveSelectedGeneratorCommand = ReactiveCommand.Create(RemoveSelectedGenerator);

            EditGeneratorCommand = ReactiveCommand.Create(EditGenerator);
            GenerateCommand = ReactiveCommand.Create(Generate);
            AcceptCommand = ReactiveCommand.Create(Accept);

            _listGenerators
                .Connect()
                .ObserveOnDispatcher()
                .Transform(x => new ListGeneratorLineViewModel(x))
                .Bind(out _listGeneratorsViewModels)
                .DisposeMany()
                .Subscribe();
        }

        #endregion Constructors

        #region Command functions

        private void AddNewGenerator()
        {
            _listGenerators.Add(new CustomListGenerator());
        }

        private void RemoveSelectedGenerator()
        {
            _listGenerators.Remove(SelectedListGenerator.ListGenerator);
        }

        private void ClearAllGenerators()
        {
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
            }
        }

        private void Generate()
        {

        }

        private void Accept()
        {
            DialogResult = SelectedListGenerator != null;
        }

        #endregion Command functions
    }
}
