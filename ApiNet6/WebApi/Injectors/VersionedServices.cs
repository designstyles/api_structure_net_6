using Backend.Application.Services.v1;
using Backend.Domain.Interfaces.Services.v1;

namespace WebApi.Injectors
{
    public static class VersionedServices
    {
        public static IServiceCollection AddServices_v1(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            return services;

        }
    }
}