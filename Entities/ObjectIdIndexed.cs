using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class ObjectIdIndexed
    {
        public ObjectIdIndexed()
        {
            DateCreated = DateTime.UtcNow;
            UId = ObjectId.GenerateNewId().ToByteArray();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public byte[] UId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}