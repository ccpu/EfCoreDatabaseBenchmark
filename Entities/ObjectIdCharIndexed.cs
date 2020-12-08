using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class ObjectIdCharIndexed
    {
        public ObjectIdCharIndexed()
        {
            DateCreated = DateTime.UtcNow;
            UId = ObjectId.GenerateNewId().ToString();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(24), Column(TypeName = "char(24)")]
        public string UId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}