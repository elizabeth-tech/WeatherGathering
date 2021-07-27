using Microsoft.EntityFrameworkCore;
using WeatherGathering.DAL.Entities.Base;

namespace WeatherGathering.DAL
{
    // Класс источника данных
    [Index(nameof(Name), IsUnique = true)] // говорим EF, что для данной сущности нужна индексация по колонке Name с требованием уникальности
    public class DataSource : NamedEntity
    {
        public string Description { get; set; }
    }
}
