using System;
using System.Threading.Tasks;

namespace EfCoreDatabaseBenchmark.Repositories
{
    public interface IRepository : IDisposable
    {
        void SelectAutoIncrementKey(int position);
        void SelectObjectIdKey(int position);
        void SelectGuidKey(int position);
        void SelectGuidSequentialKey(int position);
        void SelectCombGuidKey(int position);
        void SelectNumericIndexed(int position);
        void SelectGuidIndexed(int position);
        void SelectObjectIdCharIndexed(int position);
        void SelectObjectIdIndexed(int position);
        void SelectGuidSequentialIndexed(int position);
        void SelectCombGuidIndexed(int position);
        Task InsertAutoIncrementKey(int NumOfItems);
        Task InsertObjectIdCharIndexed(int NumOfItems);
        Task InsertObjectIdKey(int NumOfItems);
        Task InsertGuidKey(int NumOfItems);
        Task InsertNumericIndexed(int NumOfItems);
        Task InsertObjectIdIndexed(int NumOfItems);
        Task InsertGuidSequentialKey(int NumOfItems);
        Task InsertCombGuidKey(int NumOfItems);
        Task InsertGuidSequentialIndexed(int NumOfItems);
        Task InsertGuidIndexed(int NumOfItems);
        Task InsertCombGuidIndexed(int NumOfItems);
    }
}
