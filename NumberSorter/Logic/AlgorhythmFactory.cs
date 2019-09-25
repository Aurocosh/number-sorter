using NumberSorter.Algorhythm;
using NumberSorter.Logic.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic
{
    public class AlgorhythmFactory
    {
        public ISortAlgorhythm GetAlgorhythm(AlgorhythmType algorhythmType)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.BubbleSort:
                    return new BubbleSortAlgorhythm();
                case AlgorhythmType.MergeSort:
                    return new BubbleSortAlgorhythm();
                case AlgorhythmType.QuickSort:
                    return new BubbleSortAlgorhythm();
                default:
                    return null;
            }
        }
    }
}