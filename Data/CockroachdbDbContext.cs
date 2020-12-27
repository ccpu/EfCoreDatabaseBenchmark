using System;
using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDatabaseBenchmark.Data
{
    public class CockroachdbDbContext : BenchmarkDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Host=localhost;Port=26257;database=Benchmark;Username=root;";
            optionsBuilder.UseNpgsql(connectionString, options => options.SetPostgresVersion(new Version(9, 6))).UseSnakeCaseNamingConvention();
        }
    }
}
