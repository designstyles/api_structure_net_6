using Backend.Domain.Models.v1.dto;

namespace Backend.Domain.Interfaces.Services.v1
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast_Dto> Forecasts();
        IEnumerable<WeatherForecast_Dto> UseToThrowException();
    }
}
