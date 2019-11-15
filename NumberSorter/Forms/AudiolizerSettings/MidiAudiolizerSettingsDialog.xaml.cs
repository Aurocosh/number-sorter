using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class MidiAudiolizerSettingsDialog : ReactiveWindow<MidiAudiolizerSettingsDialogViewModel>
    {
        public MidiAudiolizerSettingsDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.MinNote, x => x.MinNoteIntegerUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.MaxNote, x => x.MaxNoteIntegerUpDown.Value)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.InstrumentTypes, x => x.InstrumentsComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedInstrumentType, x => x.InstrumentsComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
