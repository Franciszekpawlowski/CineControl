using Microsoft.EntityFrameworkCore;

namespace CineControl.IdentityService.API.Data;

public static class MigrationsExtension
{
    public static WebApplication applyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var _db = scope.ServiceProvider.GetRequiredService<appdbContext>();
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        return app;
    }
}
