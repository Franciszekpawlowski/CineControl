using Microsoft.EntityFrameworkCore;

namespace CineControl.CinemaService.API.Data
{
    public static class MigrationsExtension
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        { 
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<CinemaContext>();
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            return app;
        }
    }
}
