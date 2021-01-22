using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreDatabaseBenchmark.Entities
{
    public class BenchmarkCommon
    {

        public BenchmarkCommon()
        {
            Random rnd = new Random();
            RandomNumber = rnd.Next(1, 100000);
            DateCreated = DateTime.UtcNow;
        }

        public Guid SessionId { get; set; }

        public long RandomNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}
