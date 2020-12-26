
using System;
using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDatabaseBenchmark.Data
{

    public class MysqlBenchmarkContext : DbContext
    {


        public DbSet<AutoIncrementKey> AutoIncrementKey { get; set; }
        public DbSet<GuidKey> GuidKey { get; set; }
        public DbSet<GuidSequentialKey> GuidSequentialKey { get; set; }
        public DbSet<CombGuidKey> CombGuidKey { get; set; }
        public DbSet<ObjectIdKey> ObjectIdKey { get; set; }

        public DbSet<GuidIndexed> GuidIndexed { get; set; }
        public DbSet<GuidSequentialIndexed> GuidSequentialIndexed { get; set; }
        public DbSet<CombGuidIndexed> CombGuidIndexed { get; set; }
        public DbSet<NumericIndexed> NumericIndexed { get; set; }
        public DbSet<ObjectIdIndexed> ObjectIdIndexed { get; set; }
        public DbSet<ObjectIdCharIndexed> ObjectIdCharIndexed { get; set; }

        public DbSet<BenchmarkResult> Results { get; set; }

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
            modelBuilder.Entity<GuidIndexed>()
                .HasIndex(p => new { p.Guid })
                .IsUnique();

            modelBuilder.Entity<CombGuidIndexed>()
                .HasIndex(p => new { p.Guid })
                .IsUnique();

            modelBuilder.Entity<GuidSequentialIndexed>()
                .HasIndex(p => new { p.Guid })
                .IsUnique();

            modelBuilder.Entity<NumericIndexed>()
                .HasIndex(p => new { p.Num })
                .IsUnique();

            modelBuilder.Entity<ObjectIdIndexed>()
                .HasIndex(p => new { p.UId })
                .IsUnique();

            modelBuilder.Entity<ObjectIdCharIndexed>()
                .HasIndex(p => new { p.UId })
                .IsUnique();
        }
    }
}