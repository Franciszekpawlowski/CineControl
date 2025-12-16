namespace CineControl.Common.Tenant
{
    public interface ITenantProvider
    {
        void SetTenant(Guid tenantId);
        bool HasTenant();
        Guid GetTenantId();
    }
}
