using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherGathering.DAL.Context;
using WeatherGathering.DAL.Entities.Base;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataDbContext _dbContext;

        protected DbSet<T> Set { get; }

        protected virtual IQueryable<T> Items => Set;

        public DbRepository(DataDbContext dbContext)
        {
            _dbContext = dbContext;
            Set = _dbContext.Set<T>();
        }

        public async Task<bool> ExistId(int id, CancellationToken cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == id, cancel).ConfigureAwait(false);
        }

        public async Task<bool> Exist(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            return await Items.AnyAsync(item => item.Id == item.Id, cancel).ConfigureAwait(false);
        }

        public async Task<int> GetCount(CancellationToken cancel = default)
        {
            return await Items.CountAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            return await Items.ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetSkip(int skip, int count, CancellationToken cancel = default)
        {
            if (count <= 0)
                return Enumerable.Empty<T>();

            IQueryable<T> query = Items switch
            {
                // Если пришла упорядоченная последовательность, то с ней дальше и работаем
                IOrderedQueryable<T> ordered_query => ordered_query,

                // Если последовательность не была упорядочена, то упорядочиваем
                { } q => q.OrderBy(i => i.Id)
            };

            if (skip > 0)
                query = query.Skip(skip);

            return await query.Take(count).ToArrayAsync(cancel).ConfigureAwait(false);
        }


        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>;
        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            if (pageSize <= 0)
                return new Page(Enumerable.Empty<T>(), pageSize, pageIndex, pageSize);

            var query = Items;
            var total_count = await query.CountAsync(cancel).ConfigureAwait(false);
            if (total_count == 0)
                return new Page(Enumerable.Empty<T>(), 0, pageIndex, pageSize); // воазвращаем страницу, на которой ничего нет

            // Если номер страницы > 0, то мы добавляем пропуск начального кол-ва элементов
            if (pageIndex > 0)
                query = query.Skip(pageIndex * pageSize);

            query = query.Take(pageSize);
            var items = await query.ToArrayAsync(cancel).ConfigureAwait(false);
            return new Page(items, total_count, pageIndex, pageSize);
        }

        public async Task<T> GetById(int id, CancellationToken cancel = default)
        {
            return await Items.FirstOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);
        }

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.Entry(item).State = EntityState.Added;

            await _dbContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<T> Delete(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (!await ExistId(item.Id, cancel))
                return null;

            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Id == id);
            if (item is null)
                item = await Set
                    .Select(i => new T { Id = i.Id })
                    .FirstOrDefaultAsync(i => i.Id == id, cancel)
                    .ConfigureAwait(false);

            if (item is null) return null;

            return await Delete(item, cancel).ConfigureAwait(false);
        }
    }
}
