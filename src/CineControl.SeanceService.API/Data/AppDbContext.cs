using CineControl.SeanceService.API.Models;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CineControl.SeanceService.API.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public DbSet<Seance> Seances { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Movie)
                .WithMany()
                .HasForeignKey(s => s.MovieId);

            // Konfiguracja multi-tenancy
            modelBuilder.Entity<Movie>().HasQueryFilter(m => m.TenantID == _tenantProvider.TenantId);
            modelBuilder.Entity<Seance>().HasQueryFilter(s => s.TenantId == _tenantProvider.TenantId);

            base.OnModelCreating(modelBuilder);
        }
    }
}