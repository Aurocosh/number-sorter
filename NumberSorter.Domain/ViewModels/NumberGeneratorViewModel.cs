using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using ReactiveUI.Fody.Helpers;
using NumberSorter.Core.Generators;

namespace NumberSorter.Domain.ViewModels
{
    public class NumberGeneratorViewModel : ReactiveObject
    {
        #region Fields

        private List<int> _numbers = new List<int>();

        #endregion

        #region Properties

        [Reactive] public int Minimum { get; set; }
        [Reactive] public int Maximum { get; set; }
        [Reactive] public int NumberCount { get; set; }
        [Reactive] public bool? DialogResult { get; set; }

        public List<int> Numbers => new List<int>(_numbers);

        #endregion Properties


        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands


        #region Constructors

        public NumberGeneratorViewModel()
        {
            Minimum = -100;
            Maximum = 100;
            NumberCount = 100;

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
            _numbers = generator.Generate(Minimum, Maximum, NumberCount);
            DialogResult = true;
        }

        #endregion Command functions

    }
}
