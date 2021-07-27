using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherGathering.DAL.Context;
using WeatherGathering.DAL.Entities.Base;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.DAL.Repositories
{
    public class DbNamedEntityRepository<T> : DbRepository<T>, INamedEntityRepository<T> where T : NamedEntity, new()
    {
        public DbNamedEntityRepository(DataDbContext dbContext) : base(dbContext) { }

        public async Task<T> DeleteByName(string name, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Name == name);
            if (item is null)
                item = await Set
                    .Select(i => new T { Name = i.Name })
                    .FirstOrDefaultAsync(i => i.Name == name, cancel)
                    .ConfigureAwait(false);

            if (item is null) return null;

            return await Delete(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistName(string name, CancellationToken cancel = default) => await Items
            .AnyAsync(item => item.Name == name, cancel)
            .ConfigureAwait(false);

        public async Task<T> GetByName(string name, CancellationToken cancel = default) => await Items
            .FirstOrDefaultAsync(item => item.Name == name, cancel)
            .ConfigureAwait(false);
    }
}
