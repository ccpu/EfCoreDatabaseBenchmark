using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public class AutoMigration : IOperationReporter
    {
        public readonly DbContext db;


        public enum MigrationResult
        {
            Noop,
            Created,
            Migrated,
            AutoMigrated
        }

        public AutoMigration(DbContext db)
        {
            this.db = db;
        }

        public bool AllowDestructive { get; set; } = true;
        public bool MigrateNewDatabase { get; set; } = false;

        void IOperationReporter.WriteError(string message) => Console.WriteLine(message);
        void IOperationReporter.WriteInformation(string message) => Console.WriteLine(message);
        void IOperationReporter.WriteVerbose(string message) => Console.WriteLine(message);
        void IOperationReporter.WriteWarning(string message) => Console.WriteLine(message);

        public DbCommand NewCmd()
        {
            var conn = db.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.Transaction = db.Database.CurrentTransaction?.GetDbTransaction();
            return cmd;
        }

        // load the last model snapshot from the database
        public async Task<string> ReadSnapshotSource()
        {
            using var cmd = NewCmd();
            cmd.CommandText = "select snapshot from auto_migration";

            await db.Database.OpenConnectionAsync();
            try
            {
                using var reader = cmd.ExecuteReader();

                if (!await reader.ReadAsync())
                    return null;

                using var stream = new GZipStream(reader.GetStream(0), CompressionMode.Decompress);
                return await new StreamReader(stream).ReadToEndAsync();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                await db.Database.CloseConnectionAsync();
                cmd.Dispose();
            }
        }

        public async Task WriteSnapshotSource(string source)
        {
            // write snapshot into the database
            await db.Database.ExecuteSqlRawAsync(
                @"CREATE TABLE IF NOT EXISTS `auto_migration` (
              snapshot LONGBLOB NULL
          );"
            );
            await db.Database.ExecuteSqlRawAsync(
                @"insert into auto_migration(snapshot) select null where not exists(select 1 from auto_migration)"
            );

            using (var dbStream = new MemoryStream())
            {
                using (var blobStream = new GZipStream(dbStream, CompressionLevel.Fastest, true))
                {
                    await blobStream.WriteAsync(Encoding.UTF8.GetBytes(source));
                }

                dbStream.Seek(0, SeekOrigin.Begin);
                await db.Database.ExecuteSqlInterpolatedAsync($"update auto_migration set snapshot = {dbStream.ToArray()}");
            }
        }

        public T Compile<T>(string source, IEnumerable<Assembly> references)
        {
            var options = CSharpParseOptions.Default
                .WithLanguageVersion(LanguageVersion.Latest);

            var compileOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithAssemblyIdentityComparer(DesktopAssemblyIdentityComparer.Default);

            var compilation = CSharpCompilation.Create("Dynamic",
                new[] { SyntaxFactory.ParseSyntaxTree(source, options) },
                references.Select(a => MetadataReference.CreateFromFile(a.Location)),
                compileOptions
            );

            using (var ms = new MemoryStream())
            {
                var e = compilation.Emit(ms);
                if (!e.Success)
                    throw new Exception("Compilation failed");
                ms.Seek(0, SeekOrigin.Begin);

                var context = new AssemblyLoadContext(null, true);
                var assembly = context.LoadFromStream(ms);

                var modelType = assembly.DefinedTypes.Single(t => typeof(T).IsAssignableFrom(t));

                return (T)Activator.CreateInstance(modelType);
            }
        }

        public ModelSnapshot CompileSnapshot(Assembly migrationAssembly, string source) =>
            Compile<ModelSnapshot>(source, new HashSet<Assembly>() {
                AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "netstandard"),
                typeof(object).Assembly,
                typeof(DbContext).Assembly,
                migrationAssembly,
                db.GetType().Assembly,
                typeof(DbContextAttribute).Assembly,
                typeof(ModelSnapshot).Assembly,
                typeof(SqlServerValueGenerationStrategy).Assembly,
                typeof(AssemblyTargetedPatchBandAttribute).Assembly
            });

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "Just because")]
        public async Task<string> AutoMigrate(Assembly migrationAssembly, IModel oldModel, IModel newModel)
        {
            var builder = new DesignTimeServicesBuilder(migrationAssembly, Assembly.GetEntryAssembly(), this, null);
            var services = builder.Build(db);
            var dependencies = services.GetRequiredService<MigrationsScaffolderDependencies>();
            var name = dependencies.MigrationsIdGenerator.GenerateId("Auto");

            // insert an extra step to track the history of auto migrations
            var insert = dependencies.HistoryRepository.GetInsertScript(
                    new HistoryRow(
                        name,
                        (string)newModel.FindAnnotation("ProductVersion")?.Value ?? "Unknown version"
                    ));

            if (oldModel == null)
            {
                await db.Database.EnsureCreatedAsync();
                await db.Database.ExecuteSqlRawAsync(dependencies.HistoryRepository.GetCreateScript());
                await db.Database.ExecuteSqlRawAsync(insert);
            }
            else
            {
                // apply fixes for upgrading between major / minor versions
                oldModel = dependencies.SnapshotModelProcessor.Process(oldModel);

                var operations = dependencies.MigrationsModelDiffer
                    .GetDifferences(oldModel, newModel)
                    // Ignore all seed updates. Workaround for (https://github.com/aspnet/EntityFrameworkCore/issues/18943)
                    .Where(o => !(o is UpdateDataOperation))
                    .ToList();

                if (!operations.Any())
                    return null;

                if (!AllowDestructive && operations.Any(o => o.IsDestructiveChange))
                    throw new InvalidOperationException(
                        "Automatic migration was not applied because it could result in data loss.");

                operations.Add(
                    new SqlOperation()
                    {
                        Sql = insert
                    });

                // Convert the operations to sql, then execute the operations
                var sqlGenerator = db.GetService<IMigrationsSqlGenerator>();
                var commands = sqlGenerator.Generate(operations, db.Model);
                var executor = db.GetService<IMigrationCommandExecutor>();

                await executor.ExecuteNonQueryAsync(commands, db.GetService<IRelationalConnection>());
            }

            var codeGen = dependencies.MigrationsCodeGeneratorSelector.Select(null);
            return codeGen.GenerateSnapshot("AutoMigrations", db.GetType(), $"Migration_{name}", newModel);
        }

        // Migrate the database by first applying release migrations, then by auto migrating from the model snapshot stored in the database
        public async Task<MigrationResult> Migrate()
        {
            var ret = MigrationResult.Noop;

            var migrationAssembly = db.GetService<IMigrationsAssembly>();
            var migrations = db.Database.GetMigrations();
            var appliedMigrations = (await db.Database.GetAppliedMigrationsAsync()).ToList();
            var migrateDatabase = MigrateNewDatabase || migrations.Intersect(appliedMigrations).Any();

            var pendingMigrations = migrateDatabase && migrations.Except(appliedMigrations).Any();
            var devMigration = appliedMigrations.Except(migrations).LastOrDefault();

            ModelSnapshot modelSnapshot = null;
            if (devMigration != null)
            {
                if (pendingMigrations)
                    throw new InvalidOperationException("An automatic migration has been run, but you've added new release migration(s).\nYou'll need to restore from a release database.");

                var source = await ReadSnapshotSource();
                if (source == null || !source.Contains(devMigration))
                    throw new InvalidOperationException($"Expected to find the source code of the {devMigration} ModelSnapshot stored in the database");

                modelSnapshot = CompileSnapshot(migrationAssembly.Assembly, source);
            }
            else
            {
                if (migrateDatabase)
                {
                    if (pendingMigrations)
                    {
                        // Run release migrations
                        await db.Database.MigrateAsync();
                        ret = MigrationResult.Migrated;
                    }

                    modelSnapshot = migrationAssembly.ModelSnapshot;
                }
            }

            var newSnapshot = await AutoMigrate(migrationAssembly.Assembly, modelSnapshot?.Model, db.Model);
            if (newSnapshot == null) return ret;
            ret = appliedMigrations.Any() ? MigrationResult.AutoMigrated : MigrationResult.Created;
            await WriteSnapshotSource(newSnapshot);

            return ret;
        }
    }
}
