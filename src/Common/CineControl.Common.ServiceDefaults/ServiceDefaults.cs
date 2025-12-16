using Microsoft.Extensions.Hosting;

namespace CineControl.Common.ServiceDefaults;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder,string SwaggerAppName)
    {
        builder.AddJwtAuth();
        builder.AddAuthorization();
        builder.AddLogger();
        builder.Services.AddSwaggers(SwaggerAppName);
        return builder;
    }
}
