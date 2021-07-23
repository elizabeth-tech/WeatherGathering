using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.DAL.Entities.Base
{
    class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; }
    }
}
