using System.Linq;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Entities;

namespace EfCoreDatabaseBenchmark.Repositories
{
    internal class MysqlRepository : IRepository
    {
        private readonly MysqlBenchmarkContext _context;

        public MysqlRepository(MysqlBenchmarkContext context)
        {
            _context = context;
        }

        public void SelectAutoIncrementKey(int position)
        {
            var result = _context.AutoIncrementKey.Skip(position).Take(1);
            _context.AutoIncrementKey.Where(x => x.Id == result.First().Id).ToList();
        }

        public void SelectObjectIdKey(int position)
        {
            var result = _context.ObjectIdKey.Skip(position).Take(1);
            _context.ObjectIdKey.Where(x => x.Id == result.First().Id).ToList();
        }

        public void SelectGuidKey(int position)
        {
            var result = _context.GuidKey.Skip(position).Take(1);
            _context.GuidKey.Where(x => x.Id == result.First().Id).ToList();
        }

        public void SelectGuidSequentialKey(int position)
        {
            var result = _context.GuidSequentialKey.Skip(position).Take(1);
            _context.GuidSequentialKey.Where(x => x.Id == result.First().Id).ToList();
        }

        public void SelectCombGuidKey(int position)
        {
            var result = _context.CombGuidKey.Skip(position).Take(1);
            _context.CombGuidKey.Where(x => x.Id == result.First().Id).ToList();
        }

        public void SelectNumericIndexed(int position)
        {
            var result = _context.NumericIndexed.Skip(position).Take(1);
            _context.NumericIndexed.Where(x => x.Num == result.First().Num).ToList();
        }

        public void SelectGuidIndexed(int position)
        {
            var result = _context.GuidIndexed.Skip(position).Take(1);
            _context.GuidIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }

        public void SelectObjectIdCharIndexed(int position)
        {
            var result = _context.ObjectIdCharIndexed.Skip(position).Take(1);
            _context.ObjectIdCharIndexed.Where(x => x.Id == result.First().Id).ToList();
        }

        public void SelectObjectIdIndexed(int position)
        {
            var result = _context.ObjectIdIndexed.Skip(position).Take(1);
            _context.ObjectIdIndexed.Where(x => x.UId == result.First().UId).ToList();
        }

        public void SelectGuidSequentialIndexed(int position)
        {
            var result = _context.GuidSequentialIndexed.Skip(position).Take(1);
            _context.GuidSequentialIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }

        public void SelectCombGuidIndexed(int position)
        {
            var result = _context.CombGuidIndexed.Skip(position).Take(1);
            _context.CombGuidIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }

        public async Task InsertAutoIncrementKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.AutoIncrementKey.Add(new AutoIncrementKey());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertObjectIdCharIndexed(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdCharIndexed.Add(new ObjectIdCharIndexed());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertObjectIdKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdKey.Add(new ObjectIdKey());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertGuidKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidKey.Add(new GuidKey());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertNumericIndexed(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.NumericIndexed.Add(new NumericIndexed());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertObjectIdIndexed(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdIndexed.Add(new ObjectIdIndexed());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertGuidSequentialKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialKey.Add(new GuidSequentialKey());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertCombGuidKey(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.CombGuidKey.Add(new CombGuidKey());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertGuidSequentialIndexed(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialIndexed.Add(new GuidSequentialIndexed());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertGuidIndexed(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidIndexed.Add(new GuidIndexed());
            }

            await _context.SaveChangesAsync();
        }

        public async Task InsertCombGuidIndexed(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.CombGuidIndexed.Add(new CombGuidIndexed());
            }

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
