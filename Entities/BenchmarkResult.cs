using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfCoreDatabaseBenchmark.PerformanceCounter;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class BenchmarkResult : CounterInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Display(Order = 1)]
        public string BenchmarkCase { get; set; }
        [Display(Order = 2)]
        public double Inserts { get; set; }
        [Display(Order = 3)]
        public double InsertTime { get; set; }
        [Display(Order = 4)]
        public double SelectTime { get; set; }
        [Display(Order = 5)]
        public double UpdateTime { get; set; }

        [Display(Order = 6)]
        public double Sum { get; set; }
        [Display(Order = 7)]
        public double SumTime { get; set; }

        [Display(Order = 8)]
        public double Min { get; set; }
        [Display(Order = 9)]
        public double MinTime { get; set; }

        [Display(Order = 10)]
        public double Max { get; set; }
        [Display(Order = 11)]
        public double MaxTime { get; set; }

        [Display(Order = 12)]
        public double Avg { get; set; }
        [Display(Order = 13)]
        public double AvgTime { get; set; }

        [Display(Order = 14)]
        public double CountSession { get; set; }
        [Display(Order = 15)]
        public double CountSessionTime { get; set; }

        [Display(Order = 16)]
        public Guid SessionId { get; set; }
    }
}