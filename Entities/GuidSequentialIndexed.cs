using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidSequentialIndexed : BenchmarkCommon
    {
        public GuidSequentialIndexed()
        {
            Guid = Utils.SequentialGuid.Create(SequentialGuid.SequentialGuidType.SequentialAsBinary);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }

    }
}