using Microsoft.EntityFrameworkCore;
using CineControl.CinemaService.API.Models;
using CineControl.Common.Tenant;

namespace CineControl.CinemaService.API.Data
{
    public partial class CinemaContext
    {
        private readonly ITenantProvider _tenantProvider;

        public CinemaContext(DbContextOptions<CinemaContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider; 
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            if (_tenantProvider != null && _tenantProvider.HasTenant)
            {
                modelBuilder.Entity<Cinema>()
                    .HasQueryFilter(c => c.TenantId == _tenantProvider.TenantId);

                modelBuilder.Entity<Theater>()
                    .HasQueryFilter(t => t.TenantId == _tenantProvider.TenantId);

                modelBuilder.Entity<Seat>()
                    .HasQueryFilter(s => s.TenantId == _tenantProvider.TenantId);
            }
        }

    }
}
