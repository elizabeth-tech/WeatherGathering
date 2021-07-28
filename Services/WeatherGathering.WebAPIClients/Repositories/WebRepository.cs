using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherGathering.Interfaces.Base.Entities;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient httpClient;

        public WebRepository(HttpClient httpClient) => this.httpClient = httpClient;

        public Task<T> Add(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistId(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetSkip(int skip, int count, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
