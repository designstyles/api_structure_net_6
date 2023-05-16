using Microsoft.OpenApi.Models;

namespace WebApi.Middleware
{
    public static class SwaggerConfigurator
    {
        private const string HeaderName = "Bearer";

        public static void ConfigureSwaggerFeature(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test Api v1", Version = "v1" });
                //c.SwaggerDoc("v2", new OpenApiInfo { Title = "Test Api v2", Version = "v2" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.OperationFilter<SwaggerDefaultHeader>();
                c.AddSecurityDefinition(HeaderName, new OpenApiSecurityScheme
                {
                    Description = $"A valid token is needed to access the endpoint. {HeaderName}: JWT Token",
                    In = ParameterLocation.Header,
                    Name = HeaderName,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = HeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = HeaderName },
                        },
                        new string[] {}
                    }
                });
                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                c.EnableAnnotations();
            });
        }
    }

}
