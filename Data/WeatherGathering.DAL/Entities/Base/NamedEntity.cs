using System.ComponentModel.DataAnnotations;
using WeatherGathering.Interfaces.Base.Entities;

namespace WeatherGathering.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
