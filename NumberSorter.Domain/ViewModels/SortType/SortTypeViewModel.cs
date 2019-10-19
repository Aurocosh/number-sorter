using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Core.Logic;
using System;
using NumberSorter.Core.Logic.Utility;

namespace NumberSorter.Domain.ViewModels
{
    public class SortTypeViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<SortTypeLineViewModel> _sortTypes = new SourceList<SortTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public SortTypeLineViewModel SelectedSortType { get; set; }
        public IEnumerable<SortTypeLineViewModel> SortTypes => _sortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public SortTypeViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var algorhythmTypes = EnumUtil.GetValues<AlgorhythmType>();
            var sortTypes = algorhythmTypes
                .Select(x => new SortTypeLineViewModel(x, AlgorhythmNamer.GetName(x)))
                .ToList();
            sortTypes.Sort((x, y) => x.Description.CompareTo(y.Description));
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
