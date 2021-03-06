using WeatherGathering.API.Controllers.Base;
using WeatherGathering.DAL;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.API.Controllers
{
    public class DataSourcesController : EntityController<DataSource>
    {
        public DataSourcesController(IRepository<DataSource> repository) : base(repository) { }
    }
}
