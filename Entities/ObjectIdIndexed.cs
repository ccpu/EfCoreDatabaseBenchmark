using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class ObjectIdIndexed : BenchmarkCommon
    {
        public ObjectIdIndexed()
        {
            UId = ObjectId.GenerateNewId().ToByteArray();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public byte[] UId { get; set; }
    }
}