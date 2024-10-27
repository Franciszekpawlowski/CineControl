using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace CineControl.Common.ServiceDefaults;

public static class AddSwagger
{
    public static IServiceCollection AddSwaggers(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new() {
                Title = "CineControl.IdentityService.API", 
                Version = "v1" });
            c.AddSecurityDefinition("Bearer", new() {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new() {
                        Reference = new() {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}
