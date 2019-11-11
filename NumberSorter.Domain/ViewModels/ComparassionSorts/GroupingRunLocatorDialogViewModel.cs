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
    public class GroupingRunLocatorDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<ComparassionSortTypeLineViewModel> _cutoffSortTypes = new SourceList<ComparassionSortTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public int MinrunValue { get; set; }
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public ComparassionSortTypeLineViewModel SelectedFinisherSortType { get; set; }
        public IEnumerable<ComparassionSortTypeLineViewModel> SortTypes => _cutoffSortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public GroupingRunLocatorDialogViewModel()
        {
            MinrunValue = 32;
            AcceptCommand = ReactiveCommand.Create(Accept);

            var algorhythmTypes = EnumUtil.GetValues<ComparassionAlgorhythmType>();
            var sortTypes = algorhythmTypes
                .Select(x => new ComparassionSortTypeLineViewModel(x, ComparassionAlgorhythmNamer.GetName(x)))
                .ToList();
            sortTypes.Sort((x, y) => x.Name.CompareTo(y.Name));
            _cutoffSortTypes.AddRange(sortTypes);

            SelectedFinisherSortType = SortTypes.First(x => x.Type == ComparassionAlgorhythmType.BinarySort);
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
