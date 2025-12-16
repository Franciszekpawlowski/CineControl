namespace CineControl.Common.Tenant
{
    public class TenantProvider : ITenantProvider
    {
        public Guid _tenantId { get; private set; } = Guid.Empty;

        public Guid GetTenantId()
        {
            return _tenantId;
        }

        public bool HasTenant() 
            => _tenantId != Guid.Empty;

        public void SetTenant(Guid tenantId)
        {
            _tenantId = tenantId;
        }
    }
}
