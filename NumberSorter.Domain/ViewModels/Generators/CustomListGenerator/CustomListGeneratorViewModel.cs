using DynamicData;
using NumberSorter.Core.CustomGenerators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace NumberSorter.Domain.ViewModels
{
    public class CustomListGeneratorViewModel : ReactiveObject
    {
        #region Field

        private readonly CustomListGenerator _listGenerator;
        private readonly SourceList<ListProcessorSet> _listProcessorsSets;
        private readonly ReadOnlyObservableCollection<ListProcessorSetViewModel> _listProcessorsSetsViewModels;

        #endregion

        #region Properties

        [Reactive] public string Name { get; set; }
        [Reactive] public bool Shuffle { get; set; }
        [Reactive] public string Description { get; set; }

        [Reactive] public ListProcessorSetViewModel SelectedListProcessorSet { get; set; }

        public ReadOnlyObservableCollection<ListProcessorSetViewModel> ListProcessorSetLineViewModels => _listProcessorsSetsViewModels;

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> MoveUpProcessorSetCommand { get; }
        public ReactiveCommand<Unit, Unit> AddListProcessorSetCommand { get; }
        public ReactiveCommand<Unit, Unit> MoveDownProcessorSetCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearAllProcessorsSetsCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelectedProcessorSetCommand { get; }

        #endregion Commands

        #region Constructors

        public CustomListGeneratorViewModel(CustomListGenerator listGenerator)
        {
            _listGenerator = listGenerator;

            Name = listGenerator.Name;
            Shuffle = listGenerator.Shuffle;
            Description = listGenerator.Description;
            _listProcessorsSets = new SourceList<ListProcessorSet>();
            _listProcessorsSets.AddRange(listGenerator.ListProcessorSets);
            listGenerator.ListProcessorSets.Clear();

            var anyProcessorSetSelected = this.WhenAnyValue(x => x.SelectedListProcessorSet).Select(x => x != null);

            AddListProcessorSetCommand = ReactiveCommand.Create(AddListProcessorSet);
            ClearAllProcessorsSetsCommand = ReactiveCommand.Create(ClearAllProcessorSets);
            MoveUpProcessorSetCommand = ReactiveCommand.Create(MoveUpProcessorSet, anyProcessorSetSelected);
            MoveDownProcessorSetCommand = ReactiveCommand.Create(MoveDownProcessorSet, anyProcessorSetSelected);
            RemoveSelectedProcessorSetCommand = ReactiveCommand.Create(RemoveSelectedProcessorSet, anyProcessorSetSelected);

            this.WhenAnyValue(x => x.Name)
                .BindTo(_listGenerator, x => x.Name);
            this.WhenAnyValue(x => x.Shuffle)
                .BindTo(_listGenerator, x => x.Shuffle);
            this.WhenAnyValue(x => x.Description)
                .BindTo(_listGenerator, x => x.Description);

            _listProcessorsSets
                .Connect()
                .ObserveOnDispatcher()
                .Transform(x => new ListProcessorSetViewModel(x))
                .Bind(out _listProcessorsSetsViewModels)
                .DisposeMany()
                .Subscribe();

            _listProcessorsSets
                .Connect()
                .Clone(_listGenerator.ListProcessorSets)
                .Subscribe();
        }

        #endregion

        #region Command functions

        private void AddListProcessorSet() => _listProcessorsSets.Add(new ListProcessorSet());
        private void ClearAllProcessorSets() => _listProcessorsSets.Clear();
        private void RemoveSelectedProcessorSet() => _listProcessorsSets.Remove(SelectedListProcessorSet.ListProcessorSet);

        private void MoveUpProcessorSet()
        {
            int selectedIndex = ListProcessorSetLineViewModels.IndexOf(SelectedListProcessorSet);
            int newIndex = selectedIndex - 1;
            if (newIndex >= 0)
                _listProcessorsSets.Move(selectedIndex, newIndex);
        }

        private void MoveDownProcessorSet()
        {
            int selectedIndex = ListProcessorSetLineViewModels.IndexOf(SelectedListProcessorSet);
            int newIndex = selectedIndex + 1;
            if (newIndex < _listProcessorsSets.Count)
                _listProcessorsSets.Move(selectedIndex, newIndex);
        }

        #endregion Command functions
    }
}
