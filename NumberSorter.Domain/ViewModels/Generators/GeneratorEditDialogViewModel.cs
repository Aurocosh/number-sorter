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
using NumberSorter.Core.CustomGenerators.Processors.Converters;
using NumberSorter.Domain.DialogService;

namespace NumberSorter.Domain.ViewModels
{
    public class GeneratorEditDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly CustomListGenerator _listGenerator;
        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly IConverterContext _converterContext = new StandardConverterContext();

        #endregion Fields

        #region Properties

        [Reactive] public CustomListGeneratorViewModel ListGenerator { get; set; }
        [Reactive] public bool? DialogResult { get; set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> GenerateCommand { get; }
        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public GeneratorEditDialogViewModel(IDialogService<ReactiveObject> dialogService, CustomListGenerator listGenerator)
        {
            _dialogService = dialogService;
            _listGenerator = (CustomListGenerator)listGenerator.Clone();

            ListGenerator = new CustomListGeneratorViewModel(_listGenerator);

            GenerateCommand = ReactiveCommand.Create(Generate);
            AcceptCommand = ReactiveCommand.Create(Accept);
        }

        #endregion Constructors

        public CustomListGenerator GetCustomListGenerator() => _listGenerator;

        #region Command functions

        private void Generate()
        {
        }

        private void Accept()
        {
            DialogResult = true;
        }

        #endregion Command functions
    }
}
