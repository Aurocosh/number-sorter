using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberSorter.Domain.Generators
{
    public class RandomPartialSortedIntegerGenerator
    {
        private readonly Random _random;

        public RandomPartialSortedIntegerGenerator()
        {
            _random = new Random();
        }

        public List<int> Generate(int minimumValue, int maximumValue, int minRunSize, int maxRunSize, int runCount, double inversionProbability, double randomRunProbability)
        {
            var runArrays = new List<int[]>(runCount);

            int runsToMake = runCount;
            while (runsToMake-- > 0)
            {
                var runSize = _random.Next(minRunSize, maxRunSize + 1);
                var runArray = new int[runSize];

                IListUtility.Randomize(runArray, minimumValue, maximumValue);

                bool isRandom = _random.NextDouble() < randomRunProbability;
                if (!isRandom)
                    Array.Sort(runArray);

                bool isInverted = _random.NextDouble() < inversionProbability;
                if (isInverted)
                    SortRunUtility.InvertRun(runArray, new SortRun(0, runArray.Length));

                runArrays.Add(runArray);
            }

            int finalSize = runArrays.Select(x => x.Length).Sum();
            var finalArray = new int[finalSize];

            int finalIndex = 0;
            foreach (int[] runArray in runArrays)
            {
                Array.Copy(runArray, 0, finalArray, finalIndex, runArray.Length);
                finalIndex += runArray.Length;
            }
            return new List<int>(finalArray);
        }
    }
}
