using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.Clients.IdentityService.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.Common.Clients.IdentityService.ServiceExtension;

public static class IdentityServiceClientExtension
{
    public static IServiceCollection AddIdentityServiceClient(this IServiceCollection services, 
        IConfiguration configuration)
        {
            services.Configure<IdentityServiceClientOptions>(GetConfiguration(configuration));
            services.AddSingleton<IIdentityServiceClient, IdentityServiceClient>();
            return services;
        }

    private static IConfiguration GetConfiguration(IConfiguration configuration) 
        => configuration.GetSection("IdentityServiceClient") 
        ?? throw new KeyNotFoundException("IdentityServiceClient");
    
}
