using NumberSorter.Core.Generators;
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
        #region Fields

        private List<int> _numbers = new List<int>();

        #endregion

        #region Properties

        [Reactive] public int Minimum { get; set; }
        [Reactive] public int Maximum { get; set; }
        [Reactive] public int MinimumRunSize { get; set; }
        [Reactive] public int MaximumRunSize { get; set; }

        [Reactive] public double InversionProbability { get; set; }
        [Reactive] public double RandomRunProbability { get; set; }
        [Reactive] public int RunCount { get; set; }
        [Reactive] public bool? DialogResult { get; set; }

        public List<int> Numbers => new List<int>(_numbers);

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public PartialSortedGeneratorViewModel()
        {
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
            _numbers = generator.Generate(Minimum, Maximum, MinimumRunSize, MaximumRunSize, RunCount, InversionProbability, RandomRunProbability);
            DialogResult = true;
        }

        #endregion Command functions

    }
}
