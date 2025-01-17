using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.Clients.CinemaService.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.Common.Clients.CinemaService.ServiceExtension;

public static class CinemaServiceClientsExtension
{

    public static IServiceCollection AddCinemaServiceClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CinemaServiceClientOptions>(GetConfiguration(configuration));
        services.AddSingleton<ICinemaClient, CinemaClient>();
        return services;
    }

    private static IConfiguration GetConfiguration(IConfiguration configuration)
        => configuration.GetSection("CinemaServiceUrl")
        ?? throw new KeyNotFoundException("CinemaServiceUrl");
}
