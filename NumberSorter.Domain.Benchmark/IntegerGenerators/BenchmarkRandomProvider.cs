using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Benchmark.IntegerGenerators
{
    public static class BenchmarkRandomProvider
    {
        private const bool _isSeedStatic = true;
        private const int _seed = 4564;

        public static Random Random => _isSeedStatic ? new Random(_seed) : new Random();
    }
}
