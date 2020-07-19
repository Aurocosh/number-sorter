using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Data
{
    [Table("benchmark")]
    internal sealed class BenchmarkResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("report_id")]
        public int ReportId { get; set; }
        [Column("display_info")]
        public string DisplayInfo { get; set; }
        [Column("namespace")]
        public string Namespace { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("method")]
        public string Method { get; set; }
        [Column("method_title")]
        public string MethodTitle { get; set; }
        [Column("parameters")]
        public string Parameters { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }

        //[Column("statistics")]
        public Statistics Statistics { get; set; }
        public Memory Memory { get; set; }

        public void FillBlanks()
        {
            if (Statistics == null)
                Statistics = new Statistics();
            if (Memory == null)
                Memory = new Memory();
        }
    }
}