using System.Threading;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EfCoreDatabaseBenchmark.Data
{
    public interface IBenchmarkDbContext
    {
        DbSet<AutoIncrementKey> AutoIncrementKey { get; set; }
        DbSet<GuidKey> GuidKey { get; set; }
        DbSet<GuidSequentialKey> GuidSequentialKey { get; set; }
        DbSet<CombGuidKey> CombGuidKey { get; set; }
        DbSet<ObjectIdKey> ObjectIdKey { get; set; }
        DbSet<GuidIndexed> GuidIndexed { get; set; }
        DbSet<GuidSequentialIndexed> GuidSequentialIndexed { get; set; }
        DbSet<CombGuidIndexed> CombGuidIndexed { get; set; }
        DbSet<NumericIndexed> NumericIndexed { get; set; }
        DbSet<ObjectIdIndexed> ObjectIdIndexed { get; set; }
        DbSet<ObjectIdCharIndexed> ObjectIdCharIndexed { get; set; }
        DbSet<BenchmarkResult> Results { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
        void Dispose();
        DatabaseFacade Database { get; }
        EntityEntry Update(object entity);
    }

}
