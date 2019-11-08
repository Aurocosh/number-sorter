﻿using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator
{
    public class TokudaGapGenerator : RecursiveGapGenerator
    {
        protected override int GetNext(int index, int previousValue)
        {
            return (int)((2.25 * previousValue) + 1);
        }
    }
}