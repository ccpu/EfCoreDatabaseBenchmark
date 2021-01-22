using System;
using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EfCoreDatabaseBenchmark.Data
{
    public class BenchmarkDbContext : DbContext, IBenchmarkDbContext
    {
        public DbSet<AutoIncrementKey> AutoIncrementKey { get; set; }
        public DbSet<GuidKey> GuidKey { get; set; }
        public DbSet<GuidSequentialKey> GuidSequentialKey { get; set; }
        public DbSet<GuidCombKey> GuidCombKey { get; set; }
        public DbSet<ObjectIdKey> ObjectIdKey { get; set; }
        public DbSet<GuidIndexed> GuidIndexed { get; set; }
        public DbSet<GuidSequentialIndexed> GuidSequentialIndexed { get; set; }
        public DbSet<GuidCombIndexed> GuidCombIndexed { get; set; }
        public DbSet<NumericIndexed> NumericIndexed { get; set; }
        public DbSet<ObjectIdIndexed> ObjectIdIndexed { get; set; }
        public DbSet<ObjectIdCharIndexed> ObjectIdCharIndexed { get; set; }
        public DbSet<BenchmarkResult> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoIncrementKey>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<GuidKey>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<GuidSequentialKey>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<GuidCombKey>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<ObjectIdKey>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<GuidIndexed>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<GuidCombIndexed>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<GuidSequentialIndexed>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<NumericIndexed>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<ObjectIdIndexed>().HasIndex(p => new { p.SessionId });
            modelBuilder.Entity<ObjectIdCharIndexed>().HasIndex(p => new { p.SessionId });

            modelBuilder.Entity<GuidIndexed>()
                .HasIndex(p => new { p.Guid })
                .IsUnique();

            modelBuilder.Entity<GuidCombIndexed>()
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
        public bool Exists()
        {
            return (this.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }
    }
}