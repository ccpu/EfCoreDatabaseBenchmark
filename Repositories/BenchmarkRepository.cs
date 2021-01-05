using System;
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
        public async Task AutoIncrementKeyInsert(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.AutoIncrementKey.Add(new AutoIncrementKey());
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

        public async Task ObjectIdKeyInsert(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdKey.Add(new ObjectIdKey());
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
        public async Task GuidKeyInsert(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidKey.Add(new GuidKey());
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
        public async Task GuidSequentialKeyInsert(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialKey.Add(new GuidSequentialKey());
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
            var result = _context.CombGuidKey.Skip(position).Take(1);
            _context.CombGuidKey.Where(x => x.Id == result.First().Id).ToList();
        }
        public async Task CombGuidKeyInsert(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.CombGuidKey.Add(new CombGuidKey());
            }

            await _context.SaveChangesAsync();
        }
        public async Task CombGuidKeyUpdateAsync(int position)
        {
            var result = _context.CombGuidKey.Skip(position).Take(1).First();
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
        public async Task NumericIndexedInsert(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.NumericIndexed.Add(new NumericIndexed());
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
        public async Task GuidIndexedInsert(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidIndexed.Add(new GuidIndexed());
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
        public async Task ObjectIdCharIndexedInsert(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdCharIndexed.Add(new ObjectIdCharIndexed());
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
        public async Task ObjectIdIndexedInsert(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdIndexed.Add(new ObjectIdIndexed());
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
        public async Task GuidSequentialIndexedInsert(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialIndexed.Add(new GuidSequentialIndexed());
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
            var result = _context.CombGuidIndexed.Skip(position).Take(1);
            _context.CombGuidIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }
        public async Task CombGuidIndexedInsert(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.CombGuidIndexed.Add(new CombGuidIndexed());
            }

            await _context.SaveChangesAsync();
        }
        public async Task CombGuidIndexedUpdateAsync(int position)
        {
            var result = _context.CombGuidIndexed.Skip(position).Take(1).First();
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

        public int Count(string caseName)
        {
            var result = _context.Results.Where(x => x.BenchmarkCase == caseName).Select(x => x.Inserts).Sum();
            return (int)result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
