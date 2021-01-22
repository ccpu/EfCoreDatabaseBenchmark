using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDatabaseBenchmark.Repositories
{
    internal class BenchmarkRepository : IRepository
    {
        private readonly IBenchmarkDbContext _context;

        public BenchmarkRepository(IBenchmarkDbContext context)
        {
            context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
            _context = context;
        }

        #region AutoIncrementKey
        public void AutoIncrementKeySelect(int position)
        {
            var result = _context.AutoIncrementKey.Skip(position).Take(1);
            _context.AutoIncrementKey.Where(x => x.Id == result.First().Id).ToList();
        }
        public async Task AutoIncrementKeyInsert(int NumOfItems, Guid sessionId)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.AutoIncrementKey.Add(new AutoIncrementKey() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task AutoIncrementKeyUpdateAsync(int position)
        {
            var result = _context.AutoIncrementKey.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region ObjectIdKey
        public void ObjectIdKeySelect(int position)
        {
            var result = _context.ObjectIdKey.Skip(position).Take(1);
            _context.ObjectIdKey.Where(x => x.Id == result.First().Id).ToList();
        }

        public async Task ObjectIdKeyInsert(int NumOfItems, Guid sessionId)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdKey.Add(new ObjectIdKey() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task ObjectIdKeyUpdateAsync(int position)
        {
            var result = _context.ObjectIdKey.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region GuidKey
        public void GuidKeySelect(int position)
        {
            var result = _context.GuidKey.Skip(position).Take(1);
            _context.GuidKey.Where(x => x.Id == result.First().Id).ToList();
        }
        public async Task GuidKeyInsert(int NumOfItems, Guid sessionId)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidKey.Add(new GuidKey() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task GuidKeyUpdateAsync(int position)
        {
            var result = _context.GuidKey.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region GuidSequentialKey
        public void GuidSequentialKeySelect(int position)
        {
            var result = _context.GuidSequentialKey.Skip(position).Take(1);
            _context.GuidSequentialKey.Where(x => x.Id == result.First().Id).ToList();
        }
        public async Task GuidSequentialKeyInsert(int NumOfItems, Guid sessionId)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialKey.Add(new GuidSequentialKey() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task GuidSequentialKeyUpdateAsync(int position)
        {
            var result = _context.GuidSequentialKey.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region CombGuidKey
        public void CombGuidKeySelect(int position)
        {
            var result = _context.GuidCombKey.Skip(position).Take(1);
            _context.GuidCombKey.Where(x => x.Id == result.First().Id).ToList();
        }
        public async Task CombGuidKeyInsert(int NumOfItems, Guid sessionId)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidCombKey.Add(new GuidCombKey() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task CombGuidKeyUpdateAsync(int position)
        {
            var result = _context.GuidCombKey.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region NumericIndexed
        public void NumericIndexedSelect(int position)
        {
            var result = _context.NumericIndexed.Skip(position).Take(1);
            _context.NumericIndexed.Where(x => x.Num == result.First().Num).ToList();
        }
        public async Task NumericIndexedInsert(int NumOfItems, Guid sessionId)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.NumericIndexed.Add(new NumericIndexed() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task NumericIndexedUpdateAsync(int position)
        {
            var result = _context.NumericIndexed.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region GuidIndexed
        public void GuidIndexedSelect(int position)
        {
            var result = _context.GuidIndexed.Skip(position).Take(1);
            _context.GuidIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }
        public async Task GuidIndexedInsert(int NumOfItems, Guid sessionId)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidIndexed.Add(new GuidIndexed() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task GuidIndexedUpdateAsync(int position)
        {
            var result = _context.GuidIndexed.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region ObjectIdCharIndexed
        public void ObjectIdCharIndexedSelect(int position)
        {
            var result = _context.ObjectIdCharIndexed.Skip(position).Take(1);
            _context.ObjectIdCharIndexed.Where(x => x.Id == result.First().Id).ToList();
        }
        public async Task ObjectIdCharIndexedInsert(int NumOfItems, Guid sessionId)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdCharIndexed.Add(new ObjectIdCharIndexed() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task ObjectIdCharIndexedUpdateAsync(int position)
        {
            var result = _context.ObjectIdCharIndexed.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region ObjectIdIndexed
        public void ObjectIdIndexedSelect(int position)
        {
            var result = _context.ObjectIdIndexed.Skip(position).Take(1);
            _context.ObjectIdIndexed.Where(x => x.UId == result.First().UId).ToList();
        }
        public async Task ObjectIdIndexedInsert(int NumOfItems, Guid sessionId)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdIndexed.Add(new ObjectIdIndexed() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task ObjectIdIndexedUpdateAsync(int position)
        {
            var result = _context.ObjectIdIndexed.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region GuidSequentialIndexed
        public void GuidSequentialIndexedSelect(int position)
        {
            var result = _context.GuidSequentialIndexed.Skip(position).Take(1);
            _context.GuidSequentialIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }
        public async Task GuidSequentialIndexedInsert(int NumOfItems, Guid sessionId)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialIndexed.Add(new GuidSequentialIndexed() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task GuidSequentialIndexedUpdateAsync(int position)
        {
            var result = _context.GuidSequentialIndexed.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region CombGuidIndexed
        public void CombGuidIndexedSelect(int position)
        {
            var result = _context.GuidCombIndexed.Skip(position).Take(1);
            _context.GuidCombIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }
        public async Task CombGuidIndexedInsert(int NumOfItems, Guid sessionId)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidCombIndexed.Add(new GuidCombIndexed() { SessionId = sessionId });
            }

            await _context.SaveChangesAsync();
        }
        public async Task CombGuidIndexedUpdateAsync(int position)
        {
            var result = _context.GuidCombIndexed.Skip(position).Take(1).First();
            result.DateCreated = DateTime.UtcNow;
            _context.Update(result);
            await _context.SaveChangesAsync();
        }
        #endregion

        public async Task Add(BenchmarkResult result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task<long> Sum(Guid sessionId, BenchmarkTables tableName)
        {
            var sum = await SelectRandomNumber(sessionId, tableName).SumAsync();
            return sum;
        }

        public async Task<long> Min(Guid sessionId, BenchmarkTables tableName)
        {
            var sum = await SelectRandomNumber(sessionId, tableName).MinAsync();
            return sum;
        }


        public async Task<long> Max(Guid sessionId, BenchmarkTables tableName)
        {
            var sum = await SelectRandomNumber(sessionId, tableName).MaxAsync();
            return sum;
        }


        public async Task<long> CountBySession(Guid sessionId, BenchmarkTables tableName)
        {
            var sum = await SelectRandomNumber(sessionId, tableName).CountAsync();
            return sum;
        }
        public async Task<long> Avg(Guid sessionId, BenchmarkTables tableName)
        {
            var sum = await SelectRandomNumber(sessionId, tableName).AverageAsync();
            return (long)sum;
        }


        public IQueryable<long> SelectRandomNumber(Guid sessionId, BenchmarkTables tableName)
        {
            switch (tableName)
            {
                case BenchmarkTables.AutoIncrementKey:
                return _context.AutoIncrementKey.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.GuidCombIndexed:
                return _context.GuidCombIndexed.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.GuidCombKey:
                return _context.GuidCombKey.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.GuidIndexed:
                return _context.GuidIndexed.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.GuidKey:
                return _context.GuidKey.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.GuidSequentialIndexed:
                return _context.GuidSequentialIndexed.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.GuidSequentialKey:
                return _context.GuidSequentialKey.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.NumericIndexed:
                return _context.NumericIndexed.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.ObjectIdCharIndexed:
                return _context.ObjectIdCharIndexed.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.ObjectIdIndexed:
                return _context.ObjectIdIndexed.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
                case BenchmarkTables.ObjectIdKey:
                return _context.ObjectIdKey.Where((x => x.SessionId == sessionId)).Select(x => x.RandomNumber);
            }

            return null;
        }


        public int Count(BenchmarkTables tableName)
        {
            var name = tableName.ToString();
            var result = _context.Results.Where(x => x.BenchmarkCase == name).Select(x => x.Inserts).Sum();
            return (int)result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
