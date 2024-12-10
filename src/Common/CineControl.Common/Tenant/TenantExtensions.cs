using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.Common.Tenant
{
    public static class TenantExtensions
    {
        public static IServiceCollection AddTenantProvider(this IServiceCollection services)
        {
            // services.AddScoped<TenantProvider>();
            // services.AddScoped<ITenantProvider>(sp => sp.GetRequiredService<TenantProvider>());
            services.AddScoped<ITenantProvider,TenantProvider>();
            return services;
        }

        public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TenantMiddleware>();
        }
    }
}
