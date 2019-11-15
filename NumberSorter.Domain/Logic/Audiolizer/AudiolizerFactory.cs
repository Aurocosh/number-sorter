using NumberSorter.Domain.Audiolizers;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.ViewModels;
using ReactiveUI;

namespace NumberSorter.Domain.Logic
{
    public static class AudiolizerFactory
    {
        public static IStateAudiolizer GetAudiolizer(AudiolizerType type, ReactiveObject parentViewModel, IDialogService<ReactiveObject> dialogService)
        {
            switch (type)
            {
                case AudiolizerType.MidiValueAudiolizer:
                    return new MidiValueAudiolizer(30, 120, MidiInstrumentType.HonkyTonkPiano);
                case AudiolizerType.MidiValueAudiolizerCustom:
                    {
                        var viewModel = new MidiAudiolizerSettingsDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        return new MidiValueAudiolizer(viewModel.MinNote, viewModel.MaxNote, viewModel.SelectedInstrumentType.Type);
                    }
                case AudiolizerType.MidiIndexAudiolizer:
                    return new MidiIndexAudiolizer(30, 120, MidiInstrumentType.HonkyTonkPiano);
                case AudiolizerType.MidiIndexAudiolizerCustom:
                    {
                        var viewModel = new MidiAudiolizerSettingsDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        return new MidiIndexAudiolizer(viewModel.MinNote, viewModel.MaxNote, viewModel.SelectedInstrumentType.Type);
                    }
                default:
                    return null;
            }
        }
    }
}