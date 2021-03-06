using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using EfCoreDatabaseBenchmark.Utils;
using IdGen;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class NumericIndexed : BenchmarkCommon
    {
        public NumericIndexed()
        {
            Num = NumericIdGenerator.GenerateId();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long Num { get; set; }

    }
}