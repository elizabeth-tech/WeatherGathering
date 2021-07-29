using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            var response = await httpClient.PostAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            var response = await httpClient.PutAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<T> Delete(T item, CancellationToken cancel = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "")
            {
                Content = JsonContent.Create(item)
            };
            var response = await httpClient.SendAsync(request, cancel).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var response = await httpClient.DeleteAsync($"{id}", cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<bool> Exist(T item, CancellationToken cancel = default)
        {
            var response = await httpClient.PostAsJsonAsync("exist", item, cancel).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<bool> ExistId(int id, CancellationToken cancel = default)
        {
            var response = await httpClient.GetAsync($"exist/id/{id}", cancel).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<T>> GetSkip(int skip, int count, CancellationToken cancel = default) =>
            await httpClient.GetFromJsonAsync<IEnumerable<T>>($"items[{skip}:{count}]", cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default) => 
            await httpClient.GetFromJsonAsync<IEnumerable<T>>("", cancel).ConfigureAwait(false);

        public async Task<T> GetById(int id, CancellationToken cancel = default) => 
            await httpClient.GetFromJsonAsync<T>("{id}", cancel).ConfigureAwait(false);

        public async Task<int> GetCount(CancellationToken cancel = default) => 
            await httpClient.GetFromJsonAsync<int>("count", cancel).ConfigureAwait(false);

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var response = await httpClient.GetAsync($"page[{pageIndex}/{pageSize}]", cancel).ConfigureAwait(false);
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return new PageItems
                {
                    Items = Enumerable.Empty<T>(),
                    TotalCount = 0,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            return await response
                .EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<PageItems>(cancellationToken: cancel)
                .ConfigureAwait(false);
        }

        private class PageItems : IPage<T>
        {
            public IEnumerable<T> Items { get; init; }

            public int TotalCount { get; init; }

            public int PageIndex { get; init; }

            public int PageSize { get; init; }
        } 
    }
}
