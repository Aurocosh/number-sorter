using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Generators
{
    public class RandomPartialSortedIntegerGenerator : AbstractRandomGenerator
    {
        public RandomPartialSortedIntegerGenerator(Random random) : base(random)
        {
        }

        public List<int> Generate(int minimumValue, int maximumValue, int minRunSize, int maxRunSize, int runCount, double inversionProbability, double randomRunProbability)
        {
            var runArrays = new List<int[]>(runCount);

            int runsToMake = runCount;
            while (runsToMake-- > 0)
            {
                var runSize = Random.Next(minRunSize, maxRunSize + 1);
                var runArray = new int[runSize];

                ListUtility.Randomize(runArray, minimumValue, maximumValue, Random);

                bool isRandom = Random.NextDouble() < randomRunProbability;
                if (!isRandom)
                    Array.Sort(runArray);

                bool isInverted = Random.NextDouble() < inversionProbability;
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
