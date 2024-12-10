using Microsoft.EntityFrameworkCore;

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
            }
            return app;
        }
    }
}
