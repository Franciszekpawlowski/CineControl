using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CineControl.SeanceService.API.Data
{
    public static class MigrationsExtension
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        { 
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            return app;
        }
    }
}