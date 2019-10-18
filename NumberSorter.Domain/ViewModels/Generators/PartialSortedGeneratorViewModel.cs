using NumberSorter.Core.Generators;
using NumberSorter.Domain.Container;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace NumberSorter.Domain.ViewModels
{
    public class PartialSortedGeneratorViewModel : ReactiveObject
    {
        #region Properties

        [Reactive] public int Minimum { get; set; }
        [Reactive] public int Maximum { get; set; }
        [Reactive] public int MinimumRunSize { get; set; }
        [Reactive] public int MaximumRunSize { get; set; }

        [Reactive] public double InversionProbability { get; set; }
        [Reactive] public double RandomRunProbability { get; set; }
        [Reactive] public int RunCount { get; set; }
        [Reactive] public bool? DialogResult { get; set; }

        public UnsortedInput<int> InputNumbers { get; private set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public PartialSortedGeneratorViewModel()
        {
            InputNumbers = new UnsortedInput<int>();

            Minimum = -100;
            Maximum = 100;
            MinimumRunSize = 5;
            MaximumRunSize = 30;

            InversionProbability = 0.5;
            RandomRunProbability = 0.0;
            RunCount = 5;

            AcceptCommand = ReactiveCommand.Create(Accept);

            this.WhenAnyValue(x => x.Minimum)
                .Where(x => x > Maximum)
                .Subscribe(x => Maximum = x);
            this.WhenAnyValue(x => x.Maximum)
                .Where(x => x < Minimum)
                .Subscribe(x => Minimum = x);

            this.WhenAnyValue(x => x.MinimumRunSize)
                .Where(x => x > MaximumRunSize)
                .Subscribe(x => MaximumRunSize = x);
            this.WhenAnyValue(x => x.MaximumRunSize)
                .Where(x => x < MinimumRunSize)
                .Subscribe(x => MinimumRunSize = x);
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            var generator = new RandomPartialSortedIntegerGenerator(new Random());
            var numbers = generator.Generate(Minimum, Maximum, MinimumRunSize, MaximumRunSize, RunCount, InversionProbability, RandomRunProbability);
            InputNumbers = new UnsortedInput<int>(InputName, numbers);
            DialogResult = true;
        }

        #endregion Command functions

        #region Functions

        private string InputName => $"Partially sorted list. Run count: {RunCount}, Range: {Minimum} to {Maximum}";

        #endregion
    }
}
