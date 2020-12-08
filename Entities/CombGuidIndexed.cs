using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfCoreDatabaseBenchmark.Utils;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class CombGuidIndexed
    {
        public CombGuidIndexed()
        {
            DateCreated = DateTime.UtcNow;
            Guid = new GuidCombGenerator().Generate();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}