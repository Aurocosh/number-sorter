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
    public class ComparassionSortTypeViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<ComparassionSortTypeLineViewModel> _sortTypes = new SourceList<ComparassionSortTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public ComparassionSortTypeLineViewModel SelectedSortType { get; set; }
        public IEnumerable<ComparassionSortTypeLineViewModel> SortTypes => _sortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public ComparassionSortTypeViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var algorhythmTypes = EnumUtil.GetValues<ComparassionAlgorhythmType>();
            var sortTypes = algorhythmTypes
                .Select(x => new ComparassionSortTypeLineViewModel(x, ComparassionAlgorhythmNamer.GetName(x)))
                .ToList();
            sortTypes.Sort((x, y) => x.Name.CompareTo(y.Name));
            _sortTypes.AddRange(sortTypes);

            SelectedSortType = SortTypes.First();
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            DialogResult = SelectedSortType != null;
        }
        #endregion Command functions
    }
}
