using DynamicData;
using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.CustomGenerators.Processors.Converters;
using NumberSorter.Core.CustomGenerators.Processors.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace NumberSorter.Domain.ViewModels
{
    public class ListProcessorSetViewModel : ReactiveObject
    {
        #region Field

        private readonly SourceList<IListProcessor> _listProcessors;
        private readonly ReadOnlyObservableCollection<ListProcessorViewModel> _listProcessorViewModels;

        #endregion

        #region Properties

        [Reactive] public string Name { get; set; }
        [Reactive] public bool IsSameList { get; set; }
        [Reactive] public int MinRepeatValue { get; set; }
        [Reactive] public int MaxRepeatValue { get; set; }
        [Reactive] public ListProcessorViewModel SelectedListProcessor { get; set; }

        public ListProcessorSet ListProcessorSet { get; }
        public ReadOnlyObservableCollection<ListProcessorViewModel> ListProcessorViewModels => _listProcessorViewModels;

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> AddNewListProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddNewVariableListProcessorCommand { get; }

        public ReactiveCommand<Unit, Unit> AddInvertValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddShuffleValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddIntervalValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddRandomizeValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddDuplicateValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddConsequtiveValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddPartialShuffleValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddPartialConsecutiveValuesProcessorCommand { get; }

        public ReactiveCommand<Unit, Unit> ClearAllProcessorsCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelectedProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> MoveUpSelectedProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> MoveDownSelectedProcessorCommand { get; }

        #endregion

        public ListProcessorSetViewModel(ListProcessorSet listProcessorSet)
        {
            ListProcessorSet = listProcessorSet;

            Name = listProcessorSet.Name;
            IsSameList = listProcessorSet.IsSameList;
            MinRepeatValue = listProcessorSet.MinRepeatValue;
            MaxRepeatValue = listProcessorSet.MaxRepeatValue;

            _listProcessors = new SourceList<IListProcessor>();
            _listProcessors.AddRange(listProcessorSet.ListProcessors);
            listProcessorSet.ListProcessors.Clear();

            var sharedProcessorConnection = _listProcessors.Connect().Publish();

            var canClearProcessors = sharedProcessorConnection.Count().Select(x => x != 0);
            var anyProcessorSelected = this.WhenAnyValue(x => x.SelectedListProcessor).Select(x => x != null);

            AddNewListProcessorCommand = ReactiveCommand.Create(AddNewListProcessor);
            AddNewVariableListProcessorCommand = ReactiveCommand.Create(AddNewVariableListProcessor);

            AddInvertValuesProcessorCommand = ReactiveCommand.Create(AddInvertValuesProcessor);
            AddShuffleValuesProcessorCommand = ReactiveCommand.Create(AddShuffleValuesProcessor);
            AddIntervalValuesProcessorCommand = ReactiveCommand.Create(AddIntervalValuesProcessor);
            AddRandomizeValuesProcessorCommand = ReactiveCommand.Create(AddRandomizeValuesProcessor);
            AddDuplicateValuesProcessorCommand = ReactiveCommand.Create(AddDuplicateValuesProcessor);
            AddConsequtiveValuesProcessorCommand = ReactiveCommand.Create(AddConsequtiveValuesProcessor);
            AddPartialShuffleValuesProcessorCommand = ReactiveCommand.Create(AddPartialShuffleValuesProcessor);
            AddPartialConsecutiveValuesProcessorCommand = ReactiveCommand.Create(AddPartialConsecutiveValuesProcessor);

            ClearAllProcessorsCommand = ReactiveCommand.Create(ClearAllProcessors, canClearProcessors);
            RemoveSelectedProcessorCommand = ReactiveCommand.Create(RemoveSelectedProcessor, anyProcessorSelected);
            MoveUpSelectedProcessorCommand = ReactiveCommand.Create(MoveUpSelectedProcessor, anyProcessorSelected);
            MoveDownSelectedProcessorCommand = ReactiveCommand.Create(MoveDownSelectedProcessor, anyProcessorSelected);

            this.WhenAnyValue(x => x.Name)
                .BindTo(ListProcessorSet, x => x.Name);
            this.WhenAnyValue(x => x.IsSameList)
                .BindTo(ListProcessorSet, x => x.IsSameList);
            this.WhenAnyValue(x => x.MinRepeatValue)
                .BindTo(ListProcessorSet, x => x.MinRepeatValue);
            this.WhenAnyValue(x => x.MaxRepeatValue)
                .BindTo(ListProcessorSet, x => x.MaxRepeatValue);

            this.WhenAnyValue(x => x.MinRepeatValue)
                .Where(x => x > MaxRepeatValue)
                .Subscribe(x => MaxRepeatValue = x);
            this.WhenAnyValue(x => x.MaxRepeatValue)
                .Where(x => x < MinRepeatValue)
                .Subscribe(x => MinRepeatValue = x);

            _listProcessors
                .Connect()
                .ObserveOnDispatcher()
                .Transform(ConvertProcessor)
                .Bind(out _listProcessorViewModels)
                .DisposeMany()
                .Subscribe();

            _listProcessors
                .Connect()
                .Clone(ListProcessorSet.ListProcessors)
                .Subscribe();

            //_listProcessors
            //    .Connect()
            //    .BindTo(listProcessorSet, x => x.ListProcessors);
        }

        #region Command functions

        private void AddNewListProcessor() => _listProcessors.Add(new NewListProcessor());
        private void AddNewVariableListProcessor() => _listProcessors.Add(new NewVariableListProcessor());

        private void AddInvertValuesProcessor() => _listProcessors.Add(new InvertValuesProcessor());
        private void AddShuffleValuesProcessor() => _listProcessors.Add(new ShuffleValuesProcessor());
        private void AddIntervalValuesProcessor() => _listProcessors.Add(new IntervalValuesProcessor());
        private void AddRandomizeValuesProcessor() => _listProcessors.Add(new RandomizeValuesProcessor());
        private void AddDuplicateValuesProcessor() => _listProcessors.Add(new DuplicateValuesProcessor());
        private void AddConsequtiveValuesProcessor() => _listProcessors.Add(new ConsecutiveValuesProcessor());
        private void AddPartialShuffleValuesProcessor() => _listProcessors.Add(new PartialShuffleValuesProcessor());
        private void AddPartialConsecutiveValuesProcessor() => _listProcessors.Add(new PartialConsecutiveValuesProcessor());

        private void ClearAllProcessors() => _listProcessors.Clear();
        private void RemoveSelectedProcessor() => _listProcessors.Remove(SelectedListProcessor.IListProcessor);

        private void MoveUpSelectedProcessor()
        {
            int selectedIndex = ListProcessorViewModels.IndexOf(SelectedListProcessor);
            int newIndex = selectedIndex - 1;
            if (newIndex >= 0)
                _listProcessors.Move(selectedIndex, newIndex);
        }

        private void MoveDownSelectedProcessor()
        {
            int selectedIndex = ListProcessorViewModels.IndexOf(SelectedListProcessor);
            int newIndex = selectedIndex + 1;
            if (newIndex < _listProcessors.Count)
                _listProcessors.Move(selectedIndex, newIndex);
        }

        #endregion

        private ListProcessorViewModel ConvertProcessor(IListProcessor processor)
        {
            if (processor is NewListProcessor listProcessor)
                return new NewListProcessorLineViewModel(listProcessor);
            else if (processor is NewVariableListProcessor variableListProcessor)
                return new NewVariableListProcessorLineViewModel(variableListProcessor);
            else if (processor is ConsecutiveValuesProcessor consecutiveListProcessor)
                return new ConsecutiveValuesProcessorLineViewModel(consecutiveListProcessor);
            else if (processor is ShuffleValuesProcessor shuffleValuesProcessor)
                return new ShuffleValuesProcessorLineViewModel(shuffleValuesProcessor);
            else if (processor is RandomizeValuesProcessor randomizeValuesProcessor)
                return new RandomizeValuesProcessorLineViewModel(randomizeValuesProcessor);
            else if (processor is DuplicateValuesProcessor duplicateValuesProcessor)
                return new DuplicateValuesProcessorLineViewModel(duplicateValuesProcessor);
            else if (processor is PartialShuffleValuesProcessor partialShuffleValuesProcessor)
                return new PartialShuffleValuesProcessorLineViewModel(partialShuffleValuesProcessor);
            else if (processor is InvertValuesProcessor invertValuesProcessor)
                return new InvertValuesProcessorLineViewModel(invertValuesProcessor);
            else if (processor is IntervalValuesProcessor intervalValuesProcessor)
                return new IntervalValuesProcessorLineViewModel(intervalValuesProcessor);
            else if (processor is PartialConsecutiveValuesProcessor partialConsecutiveValuesProcessor)
                return new PartialConsecutiveValuesProcessorLineViewModel(partialConsecutiveValuesProcessor);
            return null;
        }
    }
}
