using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfCoreDatabaseBenchmark.Utils;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidCombIndexed : BenchmarkCommon
    {
        public GuidCombIndexed()
        {
            Guid = new GuidCombGenerator().Generate();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }

    }
}