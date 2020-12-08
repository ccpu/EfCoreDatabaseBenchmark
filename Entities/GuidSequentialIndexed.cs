using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidSequentialIndexed
    {
        public GuidSequentialIndexed()
        {
            DateCreated = DateTime.UtcNow;
            Guid = Utils.SequentialGuid.Create(SequentialGuid.SequentialGuidType.SequentialAsBinary);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}