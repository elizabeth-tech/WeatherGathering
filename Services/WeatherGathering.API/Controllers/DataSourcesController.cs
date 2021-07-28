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

        [HttpGet("exist/id/{id:int}")] // localhost:44301/api/DataSources/exist/id/56
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistId(int id) => await repository.ExistId(id) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpGet("items[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count) => 
            Ok(await repository.GetSkip(skip, count));

        // Постраничное разбиение
        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<DataSource>>> GetPage(int pageIndex, int pageSize)
        {
            var result = await repository.GetPage(pageIndex, pageSize);
            return result.Items.Any() 
                ? Ok(result) 
                : NotFound(result);
        }
    }
}
