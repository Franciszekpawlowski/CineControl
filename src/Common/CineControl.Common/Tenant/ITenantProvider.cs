using System;

namespace CineControl.Common.Tenant
{
    public interface ITenantProvider
    {
        Guid TenantId { get; }
        bool HasTenant { get; }
    }
}
