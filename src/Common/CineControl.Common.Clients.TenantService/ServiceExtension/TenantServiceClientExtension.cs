using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Clients.TenantService.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.Common.Clients.TenantService.ServiceExtension;

public static class TenantServiceClientExtension
{
    
    public static IServiceCollection AddTenantServiceClient(
        this IServiceCollection services, 
        IConfiguration configuration)
        {
            services.Configure<TenantServiceClientOptions>(GetConfiguration(configuration));
            services.AddSingleton<ITenantServiceClient, TenantServiceClient>();
            return services;
        }

    private static IConfiguration GetConfiguration(IConfiguration configuration)
        => configuration.GetSection("TenantServiceUrl") 
        ?? throw new KeyNotFoundException("TenantServiceUrl");
}
