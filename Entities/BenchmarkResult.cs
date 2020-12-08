using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfCoreDatabaseBenchmark.PerformanceCounter;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class BenchmarkResult : CounterInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Order = 1)]
        public string BenchmarkCase { get; set; }
        [Display(Order = 2)]
        public double Inserts { get; set; }
        [Display(Order = 3)]
        public double InsertTime { get; set; }
        [Display(Order = 4)]
        public double SelectTime { get; set; }
    }
}