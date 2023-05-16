namespace Backend.Domain.Models.v1.dto
{
    public class WeatherForecast_Dto
    {
        public DateTime Date { get; set; } = default;

        public int TemperatureC { get; set; } = 0;

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; } = null;
    }
}