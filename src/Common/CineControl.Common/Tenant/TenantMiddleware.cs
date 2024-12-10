using Microsoft.AspNetCore.Http;

namespace CineControl.Common.Tenant
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
        {
            if (context.Request.Headers.TryGetValue("X-TenantId", out var tenantIdValues))
            {
                if (Guid.TryParse(tenantIdValues.ToString(), out var tenantId))
                {
                    tenantProvider.SetTenant(tenantId);
                }
            }

            await _next(context);
        }
    }
}
