using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.DAL.Entities.Base
{
    public class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; }
    }
}
