using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
