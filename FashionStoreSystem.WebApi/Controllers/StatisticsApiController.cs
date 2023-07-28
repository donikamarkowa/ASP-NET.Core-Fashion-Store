using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data.Models.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreSystem.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;
        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService; 
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel = await this.statisticsService
                    .GetStatisticsAsync();

                return this.Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
