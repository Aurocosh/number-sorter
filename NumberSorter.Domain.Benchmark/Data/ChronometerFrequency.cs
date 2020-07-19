using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Data
{
    [ComplexType]
    internal sealed class ChronometerFrequency
    {
        [Column("chronometer_hertz")]
        public int Hertz { get; set; }
    }
}