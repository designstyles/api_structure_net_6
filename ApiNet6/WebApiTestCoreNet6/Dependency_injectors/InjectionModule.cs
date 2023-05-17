using Backend.Application.Services;
using Backend.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiTestCoreNet6.Dependency_injectors
{
    public static class InjectionModule
    {
        public static void ConfigureServices(ServiceCollection services)
        {
            // Register service from the library
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenValidatorService, TokenValidatorService>();
        }
    }
}
