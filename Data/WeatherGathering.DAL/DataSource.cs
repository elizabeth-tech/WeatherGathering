using WeatherGathering.DAL.Entities.Base;

namespace WeatherGathering.DAL
{
    // Класс источника данных
    class DataSource : NamedEntity
    {
        public string Description { get; set; }
    }
}
