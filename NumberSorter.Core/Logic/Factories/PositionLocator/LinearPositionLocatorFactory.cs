﻿using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PositionLocator
{
    public class LinearPositionLocatorFactory : IPositionLocatorFactory
    {
        public IPositionLocator<T> GetPositionLocator<T>(IComparer<T> comparer)
        {
            return new LinearPositionLocator<T>(comparer);
        }
    }
}
