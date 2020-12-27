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
        Task AutoIncrementKeyInsert(int NumOfItems);
        Task ObjectIdCharIndexedInsert(int NumOfItems);
        Task ObjectIdKeyInsert(int NumOfItems);
        Task GuidKeyInsert(int NumOfItems);
        Task NumericIndexedInsert(int NumOfItems);
        Task ObjectIdIndexedInsert(int NumOfItems);
        Task GuidSequentialKeyInsert(int NumOfItems);
        Task CombGuidKeyInsert(int NumOfItems);
        Task GuidSequentialIndexedInsert(int NumOfItems);
        Task GuidIndexedInsert(int NumOfItems);
        Task CombGuidIndexedInsert(int NumOfItems);
        int Count(string caseName);
        Task Add(BenchmarkResult result);
    }
}
