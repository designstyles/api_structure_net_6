using Backend.Domain.Interfaces.Services.v1;
using Backend.Domain.Models.v1.dto;

namespace Backend.Application.Services.v1
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast_Dto> Forecasts()
        {
            return Enumerable.Range(1, 10).Select(index => new WeatherForecast_Dto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            })
            .ToArray();
        }

        public IEnumerable<WeatherForecast_Dto> UseToThrowException()
        {
            throw new NotImplementedException();
        }
    }
}
