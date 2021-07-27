using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WeatherGathering.DAL;
using WeatherGathering.DAL.Context;

namespace WeatherGathering.API.Data
{
    public class DataDbInitializer
    {
        private readonly DataDbContext _dbContext;

        public DataDbInitializer(DataDbContext dbContext) => _dbContext = dbContext;

        public void Initialize()
        {
            _dbContext.Database.Migrate(); // Применяем миграции и создаем БД, если ее нет
            if (_dbContext.Sources.Any()) return; // Если в БД есть хотя бы 1 источник, то инициализация не требуется

            var rnd = new Random();
            for (int i = 1; i <= 10; i++)
            {
                var source = new DataSource
                {
                    Name = $"Источник-{i}",
                    Description = $"Тестовый источник #{i}"
                };
                _dbContext.Sources.Add(source);

                var values = new DataValue[rnd.Next(10, 20)];
                for (var (j, count) = (0, values.Length); j < count; j++)
                {
                    var value = new DataValue
                    {
                        Source = source,
                        Time = DateTimeOffset.Now.AddDays(rnd.Next(0, 365)),
                        Value = $"{rnd.Next(10, 30)}"
                    };
                    values[j] = value;
                }
                _dbContext.AddRange(values);
            }
            _dbContext.SaveChanges();
        }
    }
}
