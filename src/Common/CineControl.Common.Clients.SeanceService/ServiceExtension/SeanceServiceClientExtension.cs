using CineControl.Common.Clients.SeanceService.IClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.Common.Clients.SeanceService.ServiceExtension;

public static class SeanceServiceExtensions
{
    public static IServiceCollection AddSeanceServiceClient(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.Configure<SeanceClient>(GetConfiguration(configuration));
            services.AddSingleton<ISeanceClient, SeanceClient>()
                    .AddSingleton<IMoviesClient, MoviesClient>();

            return services;
        }
    
    private static IConfiguration GetConfiguration(IConfiguration configuration)
        => configuration.GetSection("SeanceServiceUrl")
        ?? throw new KeyNotFoundException("SeanceServiceUrl");
}