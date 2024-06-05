using Microsoft.EntityFrameworkCore;
using CineControl.FilmService.API.Models;

namespace CineControl.FilmService.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }
    }
}
