using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CineControl.Common.ServiceDefaults;

public static class Logger
{
    public static IHostApplicationBuilder AddLogger(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger();

        builder.Services.AddSerilog();
        return builder;
    }
}
