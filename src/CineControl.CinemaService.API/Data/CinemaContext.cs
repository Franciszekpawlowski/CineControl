using Microsoft.EntityFrameworkCore;

namespace CineControl.CinemaService.API.Models
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Seat> Seats { get; set; }
    }
}
