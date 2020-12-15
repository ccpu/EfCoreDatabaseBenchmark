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
            var sequence = 10;
            var numOfItems = 100000;
            while (true)
            {
                using var context = new MysqlBenchmarkContext();
                var resultService = new MysqlResultService(context);

                Console.WriteLine();
                Console.WriteLine();

                Console.Write($"Enter number of sequence ({sequence}): ");
                var readLineSequence = Console.ReadLine();
                sequence = String.IsNullOrEmpty(readLineSequence) ? sequence : int.Parse(readLineSequence);

                Console.Write($"Enter number of item to insert ({numOfItems}): ");
                var readLineNumOfItems = Console.ReadLine();
                numOfItems = String.IsNullOrEmpty(readLineNumOfItems) ? numOfItems : int.Parse(readLineNumOfItems);

                var engine = new Engine(resultService, "Mysql", sequence, numOfItems);

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "auto-increment-key", InsertFunc = mySqlRepo.InsertAutoIncrementKey, SelectFunc = mySqlRepo.SelectAutoIncrementKey, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "guid-key", InsertFunc = mySqlRepo.InsertGuidKey, SelectFunc = mySqlRepo.SelectGuidKey, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "guid-sequential-key", InsertFunc = mySqlRepo.InsertGuidSequentialKey, SelectFunc = mySqlRepo.SelectGuidSequentialKey, Repo = mySqlRepo };
                });


                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "guid-comb-key", InsertFunc = mySqlRepo.InsertCombGuidKey, SelectFunc = mySqlRepo.SelectCombGuidKey, Repo = mySqlRepo };
                });


                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "objectid-key", InsertFunc = mySqlRepo.InsertObjectIdKey, SelectFunc = mySqlRepo.SelectObjectIdKey, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "objectid-char-indexed", InsertFunc = mySqlRepo.InsertObjectIdCharIndexed, SelectFunc = mySqlRepo.SelectObjectIdCharIndexed, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "numeric-indexed", InsertFunc = mySqlRepo.InsertNumericIndexed, SelectFunc = mySqlRepo.SelectNumericIndexed, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "objectid-indexed", InsertFunc = mySqlRepo.InsertObjectIdIndexed, SelectFunc = mySqlRepo.SelectObjectIdIndexed, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "guid-indexed", InsertFunc = mySqlRepo.InsertGuidIndexed, SelectFunc = mySqlRepo.SelectGuidIndexed, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "guid-sequential-indexed", InsertFunc = mySqlRepo.InsertGuidSequentialIndexed, SelectFunc = mySqlRepo.SelectGuidSequentialIndexed, Repo = mySqlRepo };
                });

                engine.Add(() =>
                {
                    var mySqlRepo = new MysqlRepository(new MysqlBenchmarkContext());
                    return new BenchmarkCase { CaseName = "guid-comb-indexed", InsertFunc = mySqlRepo.InsertCombGuidIndexed, SelectFunc = mySqlRepo.SelectCombGuidIndexed, Repo = mySqlRepo };
                });

                await engine.Run().ConfigureAwait(false);
            }
        }
    }
}
