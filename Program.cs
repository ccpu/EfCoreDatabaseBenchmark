using System;
using System.Threading.Tasks;
using Benchmark;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Services;
using EfCoreDatabaseBenchmark.Repositories;

namespace EfCoreDatabaseBenchmark
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var context = new MysqlBenchmarkContext())
            {
                await new AutoMigration(context).Migrate().ConfigureAwait(false);
            }
            await Run().ConfigureAwait(false);
        }

        private static async Task Run()
        {
            while (true)
            {
                using var context = new MysqlBenchmarkContext();
                var resultService = new MysqlResultService(context);

                Console.WriteLine();
                Console.WriteLine();

                Console.Write("Enter number of sequence: ");
                var sequence = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                Console.Write("Enter number of item to insert: ");
                var numOfItems = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                var mySqlRepo = new MysqlRepository(context);

                var engine = new Engine(resultService, "Mysql", sequence, numOfItems);

                engine.Add(new BenchmarkCase { CaseName = "auto-increment-key", InsertFunc = mySqlRepo.InsertAutoIncrementKey, SelectFunc = mySqlRepo.SelectAutoIncrementKey });
                engine.Add(new BenchmarkCase { CaseName = "guid-key", InsertFunc = mySqlRepo.InsertGuidKey, SelectFunc = mySqlRepo.SelectGuidKey });
                engine.Add(new BenchmarkCase { CaseName = "guid-sequential-key", InsertFunc = mySqlRepo.InsertGuidSequentialKey, SelectFunc = mySqlRepo.SelectGuidSequentialKey });
                engine.Add(new BenchmarkCase { CaseName = "guid-comb-key", InsertFunc = mySqlRepo.InsertCombGuidKey, SelectFunc = mySqlRepo.SelectCombGuidKey });
                engine.Add(new BenchmarkCase { CaseName = "objectid-key", InsertFunc = mySqlRepo.InsertObjectIdKey, SelectFunc = mySqlRepo.SelectObjectIdKey });

                engine.Add(new BenchmarkCase { CaseName = "objectid-char-indexed", InsertFunc = mySqlRepo.InsertObjectIdCharIndexed, SelectFunc = mySqlRepo.SelectObjectIdCharIndexed });
                engine.Add(new BenchmarkCase { CaseName = "numeric-indexed", InsertFunc = mySqlRepo.InsertNumericIndexed, SelectFunc = mySqlRepo.SelectNumericIndexed });
                engine.Add(new BenchmarkCase { CaseName = "objectid-indexed", InsertFunc = mySqlRepo.InsertObjectIdIndexed, SelectFunc = mySqlRepo.SelectObjectIdIndexed });
                engine.Add(new BenchmarkCase { CaseName = "guid-indexed", InsertFunc = mySqlRepo.InsertGuidIndexed, SelectFunc = mySqlRepo.SelectGuidIndexed });
                engine.Add(new BenchmarkCase { CaseName = "guid-sequential-indexed", InsertFunc = mySqlRepo.InsertGuidSequentialIndexed, SelectFunc = mySqlRepo.SelectGuidSequentialIndexed });
                engine.Add(new BenchmarkCase { CaseName = "guid-comb-indexed", InsertFunc = mySqlRepo.InsertCombGuidIndexed, SelectFunc = mySqlRepo.SelectCombGuidIndexed });

                await engine.Run().ConfigureAwait(false);
            }
        }
    }
}
