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
            services.AddSingleton<IAdminClient, AdminClient>();
            services.AddSingleton<IOperatorClient, OperatorClient>();
            services.AddSingleton<IAccountClient, AccountClient>();
            return services;
        }

    private static IConfiguration GetConfiguration(IConfiguration configuration) 
        => configuration.GetSection("IdentityServiceUrl") 
        ?? throw new KeyNotFoundException("IdentityServiceUrl");
    
}
