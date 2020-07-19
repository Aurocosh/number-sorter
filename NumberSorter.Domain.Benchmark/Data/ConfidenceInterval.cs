using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Data
{
    [ComplexType]
    internal sealed class ConfidenceInterval
    {
        [Column("confidence_run_count")]
        public int N { get; set; }
        [Column("confidence_mean")]
        public double Mean { get; set; }
        [Column("confidence_standard_error")]
        public double StandardError { get; set; }
        [Column("confidence_level")]
        public int Level { get; set; }
        [Column("confidence_margin")]
        public double Margin { get; set; }
        [Column("confidence_lower")]
        public double Lower { get; set; }
        [Column("confidence_upper")]
        public double Upper { get; set; }
    }
}