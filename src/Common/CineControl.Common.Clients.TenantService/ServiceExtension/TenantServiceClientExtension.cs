using CineControl.Common.Clients.TenantService.Options;
using CineControl.Common.Tenant;
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
            services.AddSingleton<ITenantProvider, TenantProvider>();
            return services;
        }

    private static IConfiguration GetConfiguration(IConfiguration configuration)
        => configuration.GetSection("TenantServiceUrl") 
        ?? throw new KeyNotFoundException("TenantServiceUrl");
}
