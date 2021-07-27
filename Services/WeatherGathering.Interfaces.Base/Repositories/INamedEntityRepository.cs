using System.Threading;
using System.Threading.Tasks;
using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.Interfaces.Base.Repositories
{
    public interface INamedEntityRepository<T> : IRepository<T> where T : INamedEntity
    {
        Task<T> ExistName(string name, CancellationToken cancel = default);

        Task<T> GetByName(string name, CancellationToken cancel = default);

        Task<T> DeleteByName(string name, CancellationToken cancel = default);
    }
    
}
