using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherGathering.DAL;
using WeatherGathering.Interfaces.Base.Repositories;

namespace WeatherGathering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourcesController : ControllerBase
    {
        private readonly IRepository<DataSource> repository;

        public DataSourcesController(IRepository<DataSource> repository) => this.repository = repository;

        [HttpGet("count")] // localhost:44301/api/DataSources/count
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await repository.GetCount());
    }
}
