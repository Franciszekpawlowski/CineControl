using CineControl.BookingService.API.Models;
using Microsoft.EntityFrameworkCore;
using CineControl.Common.Tenant;

namespace CineControl.BookingService.API.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Tickets)
                .WithOne(t => t.Reservation)
                .HasForeignKey(t => t.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => new { t.SeanceId, t.SeatId })
                .IsUnique()
                .HasDatabaseName("IX_Ticket_SeanceId_SeatId");

            // Konfiguracja multi-tenancy
            modelBuilder.Entity<Reservation>().HasQueryFilter(r => r.TenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<Ticket>().HasQueryFilter(t => t.TenantId == _tenantProvider.GetTenantId());

            base.OnModelCreating(modelBuilder);
        }
    }
}
