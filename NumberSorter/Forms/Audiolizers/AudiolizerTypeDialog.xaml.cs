using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class AudiolizerTypeDialog : ReactiveWindow<AudiolizerTypeDialogViewModel>
    {
        public AudiolizerTypeDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    x => x.AudiolizerTypes,
                    x => x.TypeList.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.SelectedAudiolizerType,
                    x => x.TypeList.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
