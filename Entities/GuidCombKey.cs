
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfCoreDatabaseBenchmark.Utils;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidCombKey : BenchmarkCommon
    {
        public GuidCombKey()
        {
            Id = new GuidCombGenerator().Generate();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

    }
}