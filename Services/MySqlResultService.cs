
using System.Linq;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Entities;

namespace EfCoreDatabaseBenchmark.Services
{
    public class MysqlResultService : IMySqlResultService
    {
        private readonly MysqlBenchmarkContext _context;

        public MysqlResultService(MysqlBenchmarkContext context)
        {
            _context = context;
        }

        public async Task Add(BenchmarkResult result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
        }

        public int Count(string caseName)
        {
            var result = _context.Results.Where(x => x.BenchmarkCase == caseName).Select(x => x.Inserts).Sum();
            return (int)result;
        }
    }
}