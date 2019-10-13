using DynamicData;
using NumberSorter.Core.CustomGenerators;
using NumberSorter.Core.CustomGenerators.Processors.Converters;
using NumberSorter.Core.CustomGenerators.Processors.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ReactiveCommand<Unit, Unit> AddNewConsequtiveListProcessorCommand { get; }

        public ReactiveCommand<Unit, Unit> AddShuffleValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddRandomizeValuesProcessorCommand { get; }
        public ReactiveCommand<Unit, Unit> AddDuplicateValuesProcessorProcessorCommand { get; }

        public ReactiveCommand<Unit, Unit> ClearAllProcessorsCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelectedProcessorCommand { get; }

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

            AddNewListProcessorCommand = ReactiveCommand.Create(AddNewListProcessor);
            AddNewVariableListProcessorCommand = ReactiveCommand.Create(AddNewVariableListProcessor);
            AddNewConsequtiveListProcessorCommand = ReactiveCommand.Create(AddNewConsequtiveListProcessor);

            AddShuffleValuesProcessorCommand = ReactiveCommand.Create(AddShuffleValuesProcessor);
            AddRandomizeValuesProcessorCommand = ReactiveCommand.Create(AddRandomizeValuesProcessor);
            AddDuplicateValuesProcessorProcessorCommand = ReactiveCommand.Create(AddDuplicateValuesProcessorProcessor);

            ClearAllProcessorsCommand = ReactiveCommand.Create(ClearAllProcessors);
            RemoveSelectedProcessorCommand = ReactiveCommand.Create(RemoveSelectedProcessor);

            this.WhenAnyValue(x => x.Name)
                .BindTo(ListProcessorSet, x => x.Name);
            this.WhenAnyValue(x => x.IsSameList)
                .BindTo(ListProcessorSet, x => x.IsSameList);
            this.WhenAnyValue(x => x.MinRepeatValue)
                .BindTo(ListProcessorSet, x => x.MinRepeatValue);
            this.WhenAnyValue(x => x.MaxRepeatValue)
                .BindTo(ListProcessorSet, x => x.MaxRepeatValue);

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
        private void AddNewConsequtiveListProcessor() => _listProcessors.Add(new NewConsecutiveListProcessor());

        private void AddShuffleValuesProcessor() => _listProcessors.Add(new ShuffleValuesProcessor());
        private void AddRandomizeValuesProcessor() => _listProcessors.Add(new RandomizeValuesProcessor());
        private void AddDuplicateValuesProcessorProcessor() => _listProcessors.Add(new DuplicateValuesProcessor());

        private void ClearAllProcessors() => _listProcessors.Remove(SelectedListProcessor.IListProcessor);
        private void RemoveSelectedProcessor() => _listProcessors.Clear();

        #endregion

        private static ListProcessorViewModel ConvertProcessor(IListProcessor processor)
        {
            if (processor is NewListProcessor listProcessor)
                return new NewListProcessorLineViewModel(listProcessor);
            else if (processor is NewVariableListProcessor variableListProcessor)
                return new NewVariableListProcessorLineViewModel(variableListProcessor);
            else if (processor is NewConsecutiveListProcessor consecutiveListProcessor)
                return new NewConsecutiveListProcessorLineViewModel(consecutiveListProcessor);
            else if (processor is ShuffleValuesProcessor shuffleValuesProcessor)
                return new ShuffleValuesProcessorLineViewModel(shuffleValuesProcessor);
            else if (processor is RandomizeValuesProcessor randomizeValuesProcessor)
                return new RandomizeValuesProcessorLineViewModel(randomizeValuesProcessor);
            else if (processor is DuplicateValuesProcessor duplicateValuesProcessor)
                return new DuplicateValuesProcessorLineViewModel(duplicateValuesProcessor);
            return null;
        }
    }
}
