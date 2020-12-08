
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfCoreDatabaseBenchmark.Utils;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class CombGuidKey
    {
        public CombGuidKey()
        {
            DateCreated = DateTime.UtcNow;
            Id = new GuidCombGenerator().Generate();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }



        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}