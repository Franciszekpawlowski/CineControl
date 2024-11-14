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
    }
}
