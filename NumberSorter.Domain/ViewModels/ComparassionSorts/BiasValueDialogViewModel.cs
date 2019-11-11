using ReactiveUI;
using System.Reactive;
using ReactiveUI.Fody.Helpers;

namespace NumberSorter.Domain.ViewModels
{
    public class BiasValueDialogViewModel : ReactiveObject
    {
        #region Properties

        [Reactive] public int BiasValue { get; set; }
        [Reactive] public bool? DialogResult { get; set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public BiasValueDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);
            BiasValue = 2;
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            DialogResult = true;
        }

        #endregion Command functions
    }
}
