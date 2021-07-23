using System;
using System.Collections.Generic;

namespace WeatherGathering.Interfaces.Base.Repositories
{
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
