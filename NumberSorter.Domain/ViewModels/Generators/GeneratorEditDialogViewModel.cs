using ReactiveUI;
using System.Reactive;
using ReactiveUI.Fody.Helpers;
using NumberSorter.Core.CustomGenerators;
using NumberSorter.Domain.DialogService;

namespace NumberSorter.Domain.ViewModels
{
    public class GeneratorEditDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly CustomListGenerator _listGenerator;
        private readonly IDialogService<ReactiveObject> _dialogService;

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
