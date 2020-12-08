using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Entities;

namespace EfCoreDatabaseBenchmark.Services
{
    public interface IResultService
    {
        Task Add(BenchmarkResult result);
        int Count(string caseName);
    }
}