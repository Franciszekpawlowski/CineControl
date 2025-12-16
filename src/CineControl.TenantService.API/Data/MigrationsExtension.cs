using Microsoft.EntityFrameworkCore;
using CineControl.TenantService.API.Models;

namespace CineControl.TenantService.API.Data
{
    public static class MigrationsExtension
    {
        public static WebApplication applyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }

                if (!_db.Tenants.Any())
                {
                    var singleKinoId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
                    var cinemaTownId = Guid.Parse("e647f619-447d-40f0-b0c6-b1a4eb17d7db");

                    _db.Tenants.AddRange(
                        new Tenant
                        {
                            Id = singleKinoId,
                            Name = "SingleKino",
                            Description = "Pierwszy tenant"
                        },
                        new Tenant
                        {
                            Id = cinemaTownId,
                            Name = "CinemaTown",
                            Description = "Drugi tenant"
                        }
                    );

                    _db.SaveChanges();
                }
            }

            return app;
        }

    }
}
