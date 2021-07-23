using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        // Есть ли сущность с указанным Id
        Task<bool> ExistId(int id, CancellationToken cancel = default);

        // Проверка сущности по Id
        Task<bool> Exist(T item, CancellationToken cancel = default);

        // Сколько сущностей в репозитории
        Task<int> GetCount(CancellationToken cancel = default);

        Task<IEnumerable<T>> GetAll(CancellationToken cancel = default);

        // Возвращаем перечисление, пропуская какое-то кол-во значений
        Task<IEnumerable<T>> GetSkip(int skip, int count, CancellationToken cancel = default);

        // Постраничное разбиение
        Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default);

        //async Task<T> GetById(int id) => (await GetAll()).FirstOrDefault(item => item.Id == id);

        Task<T> GetById(int id, CancellationToken cancel = default);

        Task<T> Add(T item, CancellationToken cancel = default);

        Task<T> Update(T item, CancellationToken cancel = default);

        Task<T> Delete(T item, CancellationToken cancel = default);

        Task<T> DeleteById(int id, CancellationToken cancel = default);
    }

    public interface IPage<T>
    {
        IEnumerable<T> Items { get; }

        // Сколько элементов во всей выборке
        int TotalCount { get; }

        int PageIndex { get; }

        int PageSize { get; }

        // Полное кол-во страниц
        int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
