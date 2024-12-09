using Microsoft.EntityFrameworkCore;
using CineControl.CinemaService.API.Models;

namespace CineControl.CinemaService.API.Data
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Seat> Seats { get; set; }

        // Deklarujemy metodę partial bez implementacji (zostanie zdefiniowana w drugim pliku)
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>()
                .HasMany(c => c.Theaters)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Theater>()
                .HasMany(t => t.Seats)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cinema>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Theater>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Seat>().Property(s => s.Id).ValueGeneratedOnAdd();

            // Wywołaj metodę partial, w której dodajemy filtry
            OnModelCreatingPartial(modelBuilder);
        }
    }
}
