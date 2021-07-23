using System.ComponentModel.DataAnnotations;

namespace WeatherGathering.Interfaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        public string Name { get; }
    }
}
