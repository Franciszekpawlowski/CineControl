using Microsoft.Extensions.Hosting;
using Serilog;

namespace CineControl.Common.ServiceDefaults;

public static class Logger
{
    public static IHostApplicationBuilder AddLogger(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Services.AddSerilog();
        return builder;
    }
}
