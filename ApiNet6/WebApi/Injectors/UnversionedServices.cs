using Backend.Application.Services;
using Backend.Domain.Interfaces.Services;

namespace WebApi.Injectors
{
    public static class UnversionedServices
    {
        public static IServiceCollection AddUnversioned_Services(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();

            return services;

        }
    }
}