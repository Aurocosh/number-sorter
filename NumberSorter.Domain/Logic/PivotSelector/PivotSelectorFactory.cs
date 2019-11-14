using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using System;

namespace NumberSorter.Domain.Logic
{
    public static class PivotSelectorFactory
    {
        public static IPivotSelectorFactory GetPivotSelector(PivotSelectorType algorhythmType)
        {
            switch (algorhythmType)
            {
                case PivotSelectorType.Last:
                    return new LastPivotSelectorFactory();
                case PivotSelectorType.First:
                    return new FirstPivotSelectorFactory();
                case PivotSelectorType.Center:
                    return new CenterPivotSelectorFactory();
                case PivotSelectorType.MedianOfThree:
                    return new MedianOfThreePivotSelectorFactory();
                case PivotSelectorType.Random:
                    return new RandomPivotSelectorFactory(new Random());
                default:
                    return null;
            }
        }
    }
}