using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for NumberGeneratorWindow.xaml
    /// </summary>
    public partial class NumberGeneratorsWindow : ReactiveWindow<NumberGeneratorsViewModel>
    {
        public NumberGeneratorsWindow()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Minimum, x => x.MinimumUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Maximum, x => x.MaximumUpDown.Value)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.NumberCount, x => x.CountUpDown.Value)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
