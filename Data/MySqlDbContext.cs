using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDatabaseBenchmark.Data
{
    public class MySqlDbContext : BenchmarkDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;port=4999;database=Benchmark;user=root;password=pass;Old Guids=true";
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(
                    connectionString
                )
            ).UseSnakeCaseNamingConvention();
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
