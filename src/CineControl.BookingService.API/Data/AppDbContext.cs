using BookingService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
