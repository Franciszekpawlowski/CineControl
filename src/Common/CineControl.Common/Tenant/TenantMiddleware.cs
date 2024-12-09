using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CineControl.Common.Tenant
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TenantProvider tenantProvider)
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
