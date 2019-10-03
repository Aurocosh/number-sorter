﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class InsertionSortTests : SortTests
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new InsertionSort<int>(comparer);
        }
    }
}
