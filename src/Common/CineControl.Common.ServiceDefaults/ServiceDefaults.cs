using Microsoft.Extensions.Hosting;

namespace CineControl.Common.ServiceDefaults;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.AddJwtAuth();
        builder.Services.AddSwaggers();
        return builder;
    }
}
