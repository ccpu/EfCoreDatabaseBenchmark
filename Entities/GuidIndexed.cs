using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class GuidIndexed
    {
        public GuidIndexed()
        {
            DateCreated = DateTime.UtcNow;
            Guid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}