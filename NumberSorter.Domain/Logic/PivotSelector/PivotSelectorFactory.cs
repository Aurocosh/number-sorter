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
                case PivotSelectorType.Random:
                    return new RandomPivotSelectorFactory(new Random());
                case PivotSelectorType.MedianOfThree:
                    return new MedianOfThreePivotSelectorFactory();
                default:
                    return null;
            }
        }
    }
}