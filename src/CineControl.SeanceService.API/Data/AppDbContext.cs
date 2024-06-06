using Microsoft.EntityFrameworkCore;
using CineControl.SeanceService.API.Models;

namespace CineControl.SeanceService.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Seance> Seances { get; set; }
        public DbSet<Film> Films { get; set; }
    }
}
