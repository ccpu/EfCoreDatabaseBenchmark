using System;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Entities;

namespace EfCoreDatabaseBenchmark.Repositories
{
    public interface IRepository : IDisposable
    {
        void AutoIncrementKeySelect(int position);
        void ObjectIdKeySelect(int position);
        void GuidKeySelect(int position);
        void GuidSequentialKeySelect(int position);
        void CombGuidKeySelect(int position);
        void NumericIndexedSelect(int position);
        void GuidIndexedSelect(int position);
        void ObjectIdCharIndexedSelect(int position);
        void ObjectIdIndexedSelect(int position);
        void GuidSequentialIndexedSelect(int position);
        void CombGuidIndexedSelect(int position);
        Task AutoIncrementKeyInsert(int NumOfItems, Guid sessionId);
        Task ObjectIdCharIndexedInsert(int NumOfItems, Guid sessionId);
        Task ObjectIdKeyInsert(int NumOfItems, Guid sessionId);
        Task GuidKeyInsert(int NumOfItems, Guid sessionId);
        Task NumericIndexedInsert(int NumOfItems, Guid sessionId);
        Task ObjectIdIndexedInsert(int NumOfItems, Guid sessionId);
        Task GuidSequentialKeyInsert(int NumOfItems, Guid sessionId);
        Task CombGuidKeyInsert(int NumOfItems, Guid sessionId);
        Task GuidSequentialIndexedInsert(int NumOfItems, Guid sessionId);
        Task GuidIndexedInsert(int NumOfItems, Guid sessionId);
        Task CombGuidIndexedInsert(int NumOfItems, Guid sessionId);
        int Count(BenchmarkTables caseName);
        Task Add(BenchmarkResult result);
        Task<long> Sum(Guid sessionId, BenchmarkTables tableName);
        Task<long> Min(Guid sessionId, BenchmarkTables tableName);
        Task<long> Max(Guid sessionId, BenchmarkTables tableName);
        Task<long> CountBySession(Guid sessionId, BenchmarkTables tableName);
        Task<long> Avg(Guid sessionId, BenchmarkTables tableName);
    }
}
