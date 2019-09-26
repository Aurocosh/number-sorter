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
                    return new BubbleSort();
                case AlgorhythmType.InsertionSort:
                    return new InsertionSort();
                case AlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSort();
                default:
                    return null;
            }
        }
    }
}