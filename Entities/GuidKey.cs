using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidKey : BenchmarkCommon
    {
        public GuidKey()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

    }
}

