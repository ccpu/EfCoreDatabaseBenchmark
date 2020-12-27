using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDatabaseBenchmark.Data
{
    public class PostgreDbContext : BenchmarkDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Host=localhost;database=Benchmark;Username=postgres;Password=password";
            optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        }
    }
}
