using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Data
{
    [ComplexType]
    internal sealed class Percentiles
    {
        [Column("percentile_0")]
        public double P0 { get; set; }
        [Column("percentile_25")]
        public double P25 { get; set; }
        [Column("percentile_50")]
        public double P50 { get; set; }
        [Column("percentile_67")]
        public double P67 { get; set; }
        [Column("percentile_80")]
        public double P80 { get; set; }
        [Column("percentile_85")]
        public double P85 { get; set; }
        [Column("percentile_90")]
        public double P90 { get; set; }
        [Column("percentile_95")]
        public double P95 { get; set; }
        [Column("percentile_100")]
        public double P100 { get; set; }
    }
}