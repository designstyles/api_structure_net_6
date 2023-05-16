using Backend.Domain.Interfaces.Services.v1;
using Backend.Domain.Models;
using Backend.Domain.Models.v1.dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Middleware;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    [CustomClassAuthorize(Backend.Domain.Common.Enums.MembershipType.GeneralUser, Backend.Domain.Common.Enums.MembershipType.Free)]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        /// <summary>
        /// Gets random weather forecasts
        /// </summary>
        /// <returns>List of Weather forecasts</returns>
        [HttpGet]
        [Route("GetWeatherForecast")]
        [ProducesResponseType(typeof(ApiResponse<WeatherForecast_Dto>), 200)]
        [SwaggerOperation(Tags = new[] { "General User Access" })]
        [CustomMethodAuthorize(Backend.Domain.Common.Enums.MembershipType.GeneralUser)]
        public IActionResult GetWeatherForecast()
        {
            var ladder = _weatherService.Forecasts();
            return Ok(ladder);
        }

        

        /// <summary>
        /// Gets random weather forecasts
        /// </summary>
        /// <returns>List of Weather forecasts</returns>
        [HttpGet]
        [Route("GetWeatherForecastException")]
        [ProducesResponseType(typeof(ApiResponse<WeatherForecast_Dto>), 200)]
        [SwaggerOperation(Tags = new[] { "General User Access" })]
        [CustomMethodAuthorize(Backend.Domain.Common.Enums.MembershipType.GeneralUser)]
        public IActionResult GetWeatherForecastException()
        {
            var ladder = _weatherService.UseToThrowException();
            return Ok(ladder);
        }

        /// <summary>
        /// Gets random weather forecasts, Limited to 2
        /// </summary>
        /// <returns>List of Weather forecasts</returns>
        [HttpGet]
        [Route("GetWeatherForecastFree")]
        [ProducesResponseType(typeof(ApiResponse<WeatherForecast_Dto>), 200)]
        [SwaggerOperation(Tags = new[] { "Free User Access" })]
        [CustomMethodAuthorize(Backend.Domain.Common.Enums.MembershipType.Free)]
        public IActionResult GetWeatherForecastFree()
        {
            var ladder = _weatherService.Forecasts().Take(2);
            return Ok(ladder);
        }
    }
}