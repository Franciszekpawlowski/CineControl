namespace CineControl.Common.Tenant
{
    public class TenantProvider : ITenantProvider
    {
        public Guid TenantId { get; private set; } = Guid.Empty;
        public bool HasTenant { get; private set; } = false;

        public void SetTenant(Guid tenantId)
        {
            TenantId = tenantId;
            HasTenant = true;
        }
    }
}
