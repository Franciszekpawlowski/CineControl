using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CineControl.Common.ServiceDefaults;

public static class Logger
{
    public static IHostApplicationBuilder AddLogger(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateLogger();

        builder.Services.AddSerilog();
        return builder;
    }
}
