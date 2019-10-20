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
using NumberSorter.Domain.Visualizers;
using NumberSorter.Domain.AppColors;
using System.Windows.Media;
using NumberSorter.Domain.Interactions;

namespace NumberSorter.Domain.ViewModels
{
    public class ColorSetDialogViewModel : ReactiveObject
    {
        #region Properties
        [Reactive] public ColorSet ColorSet { get; private set; }

        [Reactive] public string Name { get; set; }

        [Reactive] public Color ReadColor { get; set; }
        [Reactive] public Color WriteColor { get; set; }
        [Reactive] public Color NormalColor { get; set; }

        [Reactive] public Color BackgroundColor { get; set; }

        [Reactive] public Color CompareEqualColor { get; set; }
        [Reactive] public Color CompareBiggerColor { get; set; }
        [Reactive] public Color CompareLesserColor { get; set; }

        [Reactive] public bool? DialogResult { get; set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Color> EditReadColorCommand { get; }
        public ReactiveCommand<Unit, Color> EditWriteColorCommand { get; }
        public ReactiveCommand<Unit, Color> EditNormalColorCommand { get; }

        public ReactiveCommand<Unit, Color> EditBackgroundColorCommand { get; }

        public ReactiveCommand<Unit, Color> EditCompareEqualColorCommand { get; }
        public ReactiveCommand<Unit, Color> EditCompareLesserColorCommand { get; }
        public ReactiveCommand<Unit, Color> EditCompareBiggerColorCommand { get; }

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public ColorSetDialogViewModel(ColorSet colorSet)
        {
            ColorSet = colorSet;

            Name = colorSet.Name;

            ReadColor = colorSet.ReadColor;
            WriteColor = colorSet.WriteColor;
            NormalColor = colorSet.NormalColor;

            BackgroundColor = colorSet.BackgroundColor;

            CompareEqualColor = colorSet.CompareEqualColor;
            CompareLesserColor = colorSet.CompareLesserColor;
            CompareBiggerColor = colorSet.CompareBiggerColor;

            EditReadColorCommand = ReactiveCommand.CreateFromObservable(EditReadColor);
            EditWriteColorCommand = ReactiveCommand.CreateFromObservable(EditWriteColor);
            EditNormalColorCommand = ReactiveCommand.CreateFromObservable(EditNormalColor);

            EditBackgroundColorCommand = ReactiveCommand.CreateFromObservable(EditBackgroundColor);

            EditCompareEqualColorCommand = ReactiveCommand.CreateFromObservable(EditCompareEqualColor);
            EditCompareBiggerColorCommand = ReactiveCommand.CreateFromObservable(EditCompareBiggerColor);
            EditCompareLesserColorCommand = ReactiveCommand.CreateFromObservable(EditCompareLesserColor);

            AcceptCommand = ReactiveCommand.Create(Accept);

            EditReadColorCommand.Subscribe(x => ReadColor = x);
            EditWriteColorCommand.Subscribe(x => WriteColor = x);
            EditNormalColorCommand.Subscribe(x => NormalColor = x);

            EditBackgroundColorCommand.Subscribe(x => BackgroundColor = x);

            EditCompareEqualColorCommand.Subscribe(x => CompareEqualColor = x);
            EditCompareBiggerColorCommand.Subscribe(x => CompareBiggerColor = x);
            EditCompareLesserColorCommand.Subscribe(x => CompareLesserColor = x);
        }

        #endregion Constructors

        #region Command functions

        private IObservable<Color> EditReadColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.ReadColor);
        private IObservable<Color> EditWriteColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.WriteColor);
        private IObservable<Color> EditNormalColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.NormalColor);

        private IObservable<Color> EditBackgroundColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.BackgroundColor);

        private IObservable<Color> EditCompareEqualColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.CompareEqualColor);
        private IObservable<Color> EditCompareBiggerColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.CompareBiggerColor);
        private IObservable<Color> EditCompareLesserColor() => DialogInteractions.PickAnotherColor.Handle(ColorSet.CompareLesserColor);

        private void Accept()
        {
            ColorSet = new ColorSet(ColorSet.Id, Name, ReadColor, WriteColor, NormalColor, BackgroundColor, CompareEqualColor, CompareBiggerColor, CompareLesserColor);
            DialogResult = true;
        }

        #endregion Command functions
    }
}
