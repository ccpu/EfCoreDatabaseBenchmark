using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidIndexed : BenchmarkCommon
    {
        public GuidIndexed()
        {
            Guid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }

    }
}