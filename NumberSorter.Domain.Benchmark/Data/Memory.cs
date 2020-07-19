using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Data
{
    [ComplexType]
    public class Memory
    {
        [Column("memory_gen0_collections")]
        public int Gen0Collections { get; set; }
        [Column("memory_gen1_collections")]
        public int Gen1Collections { get; set; }
        [Column("memory_gen2_collections")]
        public int Gen2Collections { get; set; }
        [Column("memory_total_operations")]
        public int TotalOperations { get; set; }
        [Column("memory_bytes_allocated_per_operation")]
        public int BytesAllocatedPerOperation { get; set; }
    }
}
