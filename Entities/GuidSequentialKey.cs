using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidSequentialKey : BenchmarkCommon
    {
        public GuidSequentialKey()
        {
            Id = Utils.SequentialGuid.Create(SequentialGuid.SequentialGuidType.SequentialAsBinary);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

    }
}

