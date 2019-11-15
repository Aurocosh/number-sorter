using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Logic;

namespace NumberSorter.Domain.ViewModels
{
    public class AudiolizerTypeDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<AudiolizerTypeLineViewModel> _audiolizerTypes = new SourceList<AudiolizerTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public AudiolizerTypeLineViewModel SelectedAudiolizerType { get; set; }
        public IEnumerable<AudiolizerTypeLineViewModel> AudiolizerTypes => _audiolizerTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public AudiolizerTypeDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var audiolizerTypes = EnumUtil.GetValues<AudiolizerType>();
            var audiolizerViewModels = audiolizerTypes
                .Select(x => new AudiolizerTypeLineViewModel(x, AudiolizerNamer.GetName(x)))
                .ToList();
            audiolizerViewModels.Sort((x, y) => x.Name.CompareTo(y.Name));
            _audiolizerTypes.AddRange(audiolizerViewModels);

            SelectedAudiolizerType = AudiolizerTypes.First(x => x.Type == AudiolizerType.MidiValueAudiolizer);
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            DialogResult = SelectedAudiolizerType != null;
        }
        #endregion Command functions
    }
}
