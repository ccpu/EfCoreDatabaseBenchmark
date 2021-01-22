using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDatabaseBenchmark.Data
{
    public class MariadbDbContext : BenchmarkDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;port=4998;database=Benchmark;user=root;password=pass;Old Guids=true";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ObjectIdIndexed>().Property(
                x => x.UId
            ).HasColumnType(
                "Binary(12)"
            );
        }
    }
}
