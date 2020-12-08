using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Entities;
using System.Linq;

namespace EfCoreDatabaseBenchmark.Controllers
{

    public class MysqlRepositories
    {
        private readonly MysqlBenchmarkContext _context;

        public MysqlRepositories(MysqlBenchmarkContext context)
        {
            _context = context;

        }

        private void SelectAutoIncrementKey(int position)
        {
            var result = _context.AutoIncrementKey.Skip(position).Take(1);
            _context.AutoIncrementKey.Where(x => x.Id == result.First().Id).ToList();
        }

        private void SelectObjectIdKey(int position)
        {
            var result = _context.ObjectIdKey.Skip(position).Take(1);
            _context.ObjectIdKey.Where(x => x.Id == result.First().Id).ToList();
        }

        private void SelectGuidKey(int position)
        {
            var result = _context.GuidKey.Skip(position).Take(1);
            _context.GuidKey.Where(x => x.Id == result.First().Id).ToList();
        }

        private void SelectGuidSequentialKey(int position)
        {
            var result = _context.GuidSequentialKey.Skip(position).Take(1);
            _context.GuidSequentialKey.Where(x => x.Id == result.First().Id).ToList();
        }

        private void SelectCombGuidKey(int position)
        {
            var result = _context.CombGuidKey.Skip(position).Take(1);
            _context.CombGuidKey.Where(x => x.Id == result.First().Id).ToList();
        }

        private void SelectNumericIndexed(int position)
        {
            var result = _context.NumericIndexed.Skip(position).Take(1);
            _context.NumericIndexed.Where(x => x.Num == result.First().Num).ToList();
        }

        private void SelectGuidIndexed(int position)
        {
            var result = _context.GuidIndexed.Skip(position).Take(1);
            _context.GuidIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }

        private void SelectObjectIdCharIndexed(int position)
        {
            var result = _context.ObjectIdCharIndexed.Skip(position).Take(1);
            _context.ObjectIdCharIndexed.Where(x => x.Id == result.First().Id).ToList();
        }

        private void SelectObjectIdIndexed(int position)
        {
            var result = _context.ObjectIdIndexed.Skip(position).Take(1);
            _context.ObjectIdIndexed.Where(x => x.UId == result.First().UId).ToList();
        }

        private void SelectGuidSequentialIndexed(int position)
        {
            var result = _context.GuidSequentialIndexed.Skip(position).Take(1);
            _context.GuidSequentialIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }

        private void SelectCombGuidIndexed(int position)
        {
            var result = _context.CombGuidIndexed.Skip(position).Take(1);
            _context.CombGuidIndexed.Where(x => x.Guid == result.First().Guid).ToList();
        }

        private async Task InsertAutoIncrementKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.AutoIncrementKey.Add(new AutoIncrementKey());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertObjectIdCharIndexed(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdCharIndexed.Add(new ObjectIdCharIndexed());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertObjectIdKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdKey.Add(new ObjectIdKey());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertGuidKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidKey.Add(new GuidKey());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertNumericIndexed(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.NumericIndexed.Add(new NumericIndexed());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertObjectIdIndexed(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.ObjectIdIndexed.Add(new ObjectIdIndexed());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertGuidSequentialKey(int NumOfItems)
        {

            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialKey.Add(new GuidSequentialKey());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertCombGuidKey(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.CombGuidKey.Add(new CombGuidKey());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertGuidSequentialIndexed(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidSequentialIndexed.Add(new GuidSequentialIndexed());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertGuidIndexed(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.GuidIndexed.Add(new GuidIndexed());
            }

            await _context.SaveChangesAsync();
        }

        private async Task InsertCombGuidIndexed(int NumOfItems)
        {
            for (int i = 0; i < NumOfItems; i++)
            {
                _context.CombGuidIndexed.Add(new CombGuidIndexed());
            }

            await _context.SaveChangesAsync();
        }
    }

}
