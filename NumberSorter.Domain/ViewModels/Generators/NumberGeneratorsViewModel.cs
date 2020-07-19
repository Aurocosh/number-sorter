using System;
using System.Linq;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using ReactiveUI.Fody.Helpers;
using NumberSorter.Core.Generators;
using NumberSorter.Domain.Container;

namespace NumberSorter.Domain.ViewModels
{
    public class NumberGeneratorsViewModel : ReactiveObject
    {
        #region Properties

        [Reactive] public int Minimum { get; set; }
        [Reactive] public int Maximum { get; set; }
        [Reactive] public int NumberCount { get; set; }
        [Reactive] public bool? DialogResult { get; set; }

        public UnsortedInput<int> InputNumbers { get; private set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public NumberGeneratorsViewModel()
        {
            Minimum = -100;
            Maximum = 100;
            NumberCount = 100;

            InputNumbers = new UnsortedInput<int>();

            AcceptCommand = ReactiveCommand.Create(Accept);

            this.WhenAnyValue(x => x.Minimum)
                .Where(x => x > Maximum)
                .Subscribe(x => Maximum = x);

            this.WhenAnyValue(x => x.Maximum)
                .Where(x => x < Minimum)
                .Subscribe(x => Minimum = x);

        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            var generator = new RandomIntegerGenerator(new Random());
            var numbers = generator.Generate(Minimum, Maximum, NumberCount);
            InputNumbers = new UnsortedInput<int>(InputName, numbers);
            DialogResult = true;
        }

        #endregion Command functions

        #region Functions

        private string InputName => $"Random list. Size: {NumberCount}, Range: {Minimum} to {Maximum}";

        #endregion

    }
}
