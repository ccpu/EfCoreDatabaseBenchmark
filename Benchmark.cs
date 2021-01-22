using System;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Repositories;

namespace EfCoreDatabaseBenchmark
{
    public static class Benchmark
    {

        public static async Task Run(Func<IBenchmarkDbContext> getContext, int sequence, int numOfItems)
        {

            var context = getContext();
            context.Database.EnsureCreated();

            var engine = new Engine(sequence, numOfItems);

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.AutoIncrementKey,
                    InsertFunc = repo.AutoIncrementKeyInsert,
                    SelectFunc = repo.AutoIncrementKeySelect,
                    UpdateFunc = repo.AutoIncrementKeyUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.GuidKey,
                    InsertFunc = repo.GuidKeyInsert,
                    SelectFunc = repo.GuidKeySelect,
                    UpdateFunc = repo.GuidKeyUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.GuidSequentialKey,
                    InsertFunc = repo.GuidSequentialKeyInsert,
                    SelectFunc = repo.GuidSequentialKeySelect,
                    UpdateFunc = repo.GuidSequentialKeyUpdateAsync,
                    Repo = repo
                };
            });


            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.GuidCombKey,
                    InsertFunc = repo.CombGuidKeyInsert,
                    SelectFunc = repo.CombGuidKeySelect,
                    UpdateFunc = repo.CombGuidKeyUpdateAsync,
                    Repo = repo
                };
            });


            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.ObjectIdKey,
                    InsertFunc = repo.ObjectIdKeyInsert,
                    SelectFunc = repo.ObjectIdKeySelect,
                    UpdateFunc = repo.ObjectIdKeyUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.ObjectIdCharIndexed,
                    InsertFunc = repo.ObjectIdCharIndexedInsert,
                    SelectFunc = repo.ObjectIdCharIndexedSelect,
                    UpdateFunc = repo.ObjectIdCharIndexedUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.NumericIndexed,
                    InsertFunc = repo.NumericIndexedInsert,
                    SelectFunc = repo.NumericIndexedSelect,
                    UpdateFunc = repo.NumericIndexedUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.ObjectIdIndexed,
                    InsertFunc = repo.ObjectIdIndexedInsert,
                    SelectFunc = repo.ObjectIdIndexedSelect,
                    UpdateFunc = repo.ObjectIdIndexedUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.GuidIndexed,
                    InsertFunc = repo.GuidIndexedInsert,
                    SelectFunc = repo.GuidIndexedSelect,
                    UpdateFunc = repo.GuidIndexedUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.GuidSequentialIndexed,
                    InsertFunc = repo.GuidSequentialIndexedInsert,
                    SelectFunc = repo.GuidSequentialIndexedSelect,
                    UpdateFunc = repo.GuidSequentialIndexedUpdateAsync,
                    Repo = repo
                };
            });

            engine.Add(() =>
            {
                var repo = new BenchmarkRepository(getContext());
                return new BenchmarkCase
                {
                    TableName = BenchmarkTables.GuidCombIndexed,
                    InsertFunc = repo.CombGuidIndexedInsert,
                    SelectFunc = repo.CombGuidIndexedSelect,
                    UpdateFunc = repo.CombGuidIndexedUpdateAsync,
                    Repo = repo
                };
            });

            await engine.Run().ConfigureAwait(false);

        }

    }
}
