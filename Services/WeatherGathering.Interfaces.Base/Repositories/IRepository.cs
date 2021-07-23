using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<bool> ExistId(int id);

        Task<bool> Exist(T item);

        Task<int> GetCount();

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetSkip(int skip, int count);

        //Task<> GetPage(int pageIndex, int pageSize);

        //async Task<T> GetById(int id) => (await GetAll()).FirstOrDefault(item => item.Id == id);

        Task<T> GetById(int id);

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task<T> Delete(T item);

        Task<T> DeleteById(int id);
    }

    public interface IPage<T>
    {
        IEnumerable<T> Items { get; }

        int TotalCount { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
