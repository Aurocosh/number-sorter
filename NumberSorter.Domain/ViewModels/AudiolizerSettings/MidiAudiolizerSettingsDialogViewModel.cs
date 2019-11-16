using System;
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
    public class MidiAudiolizerSettingsDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<MidiInstrumentTypeLineViewModel> _instrumentTypes = new SourceList<MidiInstrumentTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public int MinNote { get; set; }
        [Reactive] public int MaxNote { get; set; }
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public MidiInstrumentTypeLineViewModel SelectedInstrumentType { get; set; }

        public IEnumerable<MidiInstrumentTypeLineViewModel> InstrumentTypes => _instrumentTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public MidiAudiolizerSettingsDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            MinNote = 30;
            MaxNote = 100;

            var instrumentTypes = EnumUtil.GetValues<MidiInstrumentType>()
                .Select(x => new MidiInstrumentTypeLineViewModel(x, MidiInstrumentNamer.GetName(x)))
                .ToList();
            instrumentTypes.Sort((x, y) => x.Type.CompareTo(y.Type));
            _instrumentTypes.AddRange(instrumentTypes);

            SelectedInstrumentType = InstrumentTypes.First(x => x.Type == MidiInstrumentType.HonkyTonkPiano);

            this.WhenAnyValue(x => x.MinNote)
                .Where(x => x > MaxNote)
                .Subscribe(x => MaxNote = x);
            this.WhenAnyValue(x => x.MaxNote)
                .Where(x => x < MinNote)
                .Subscribe(x => MinNote = x);
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
