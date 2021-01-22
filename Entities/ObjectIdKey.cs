using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class ObjectIdKey : BenchmarkCommon
    {
        public ObjectIdKey()
        {
            Id = ObjectId.GenerateNewId().ToByteArray();
        }

        [Key, MaxLength(12)]
        public byte[] Id { get; set; }
    }
}