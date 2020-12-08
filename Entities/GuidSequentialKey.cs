using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidSequentialKey
    {
        public GuidSequentialKey()
        {
            DateCreated = DateTime.UtcNow;
            Id = Utils.SequentialGuid.Create(SequentialGuid.SequentialGuidType.SequentialAsBinary);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}

