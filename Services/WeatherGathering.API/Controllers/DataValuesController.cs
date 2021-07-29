using WeatherGathering.API.Controllers.Base;
using WeatherGathering.DAL;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.API.Controllers
{
    public class DataValuesController : EntityController<DataValue>
    {
        public DataValuesController(IRepository<DataValue> repository) : base(repository) { }
    }
}
