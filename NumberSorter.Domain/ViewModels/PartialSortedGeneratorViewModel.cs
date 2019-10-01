using NumberSorter.Domain.Generators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

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
        [Reactive] public int MinimumRunLength { get; set; }
        [Reactive] public int MaximumRunLength { get; set; }
        [Reactive] public int MinimumRunStep { get; set; }
        [Reactive] public int MaximumRunStep { get; set; }

        [Reactive] public double RandomRunProbability { get; set; }
        [Reactive] public int NumberCount { get; set; }
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
            MinimumRunLength = 5;
            MaximumRunLength = 30;
            MinimumRunStep = 1;
            MaximumRunStep = 10;

            RandomRunProbability = 0.0;
            NumberCount = 100;

            AcceptCommand = ReactiveCommand.Create(Accept);

            this.WhenAnyValue(x => x.Minimum)
                .Where(x => x > Maximum)
                .Subscribe(x => Maximum = x);
            this.WhenAnyValue(x => x.Maximum)
                .Where(x => x < Minimum)
                .Subscribe(x => Minimum = x);

            this.WhenAnyValue(x => x.MinimumRunLength)
                .Where(x => x > MaximumRunLength)
                .Subscribe(x => MaximumRunLength = x);
            this.WhenAnyValue(x => x.MaximumRunLength)
                .Where(x => x < MinimumRunLength)
                .Subscribe(x => MinimumRunLength = x);

            this.WhenAnyValue(x => x.MinimumRunStep)
                .Where(x => x > MaximumRunStep)
                .Subscribe(x => MaximumRunStep = x);
            this.WhenAnyValue(x => x.MaximumRunStep)
                .Where(x => x < MinimumRunStep)
                .Subscribe(x => MinimumRunStep = x);

        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            var generator = new RandomPartialSortedIntegerGenerator();
            _numbers = generator.Generate(Minimum, Maximum, MinimumRunLength, MaximumRunLength, MinimumRunStep, MaximumRunStep, NumberCount, RandomRunProbability);
            DialogResult = true;
        }

        #endregion Command functions

    }
}
