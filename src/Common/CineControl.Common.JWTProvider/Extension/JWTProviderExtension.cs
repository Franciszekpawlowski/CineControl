using System;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.Common.JWTProvider.Extension;

public static class JWTProviderExtension
{
    public static IServiceCollection AddJWTProvider(this IServiceCollection services) 
        => services.AddJWTProvider();
}
