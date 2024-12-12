namespace CineControl.Common.Tenant
{
    public interface ITenantProvider
    {
        void SetTenant(Guid tenantId);
        Guid TenantId { get; }
        bool HasTenant { get; }
    }
}
