using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for PartialSortedGeneratorDialog.xaml
    /// </summary>
    public partial class PartialSortedGeneratorDialog : ReactiveWindow<PartialSortedGeneratorViewModel>
    {
        public PartialSortedGeneratorDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Minimum, x => x.MinimumUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Maximum, x => x.MaximumUpDown.Value)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.MinimumRunSize, x => x.MinimumRunLengthUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.MaximumRunSize, x => x.MaximumRunLengthUpDown.Value)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.InversionProbability, x => x.InvertedRunUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.RandomRunProbability, x => x.RandomRunUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.RunCount, x => x.CountUpDown.Value)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
