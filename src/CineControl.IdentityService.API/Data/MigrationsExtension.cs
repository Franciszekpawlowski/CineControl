using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Enums;
using CineControl.IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
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

            if(!_db.Users.Any())
            {
                SeedUsers(app);
            }
        }
        return app;
    }

    private static async Task SeedUsers(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var tenantIds = new[]
        {
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Guid.Parse("e647f619-447d-40f0-b0c6-b1a4eb17d7db")
            };

        var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        ApplicationUser tenant1user = new()
        {
            TenantId = tenantIds[0],
            UserName = "user1",
            NormalizedUserName = "user",
            Email = "user1@test.com",
            NormalizedEmail = "user1@test.com",
            EmailConfirmed = true,
        };

        await _userManager.CreateAsync(tenant1user, "YXo1?5K=:u}s");

        await _userManager.AddClaimAsync(tenant1user, new Claim(CustomClaims.Role, Roles.User.ToString()));

        ApplicationUser tenant2user = new()
        {
            TenantId = tenantIds[1],
            UserName = "user2",
            NormalizedUserName = "user2",
            Email = "user2@test.com",
            NormalizedEmail = "user2@test.com",
            EmailConfirmed = true,
        };

        await _userManager.CreateAsync(tenant2user, "YXo1?5K=:u}s");

        await _userManager.AddClaimAsync(tenant2user, new Claim(CustomClaims.Role, Roles.User.ToString()));

        ApplicationUser operatorUser1 = new()
        {
            TenantId = tenantIds[0],
            UserName = "operator1",
            NormalizedUserName = "operator1",
            Email = "operator1@test.com",
            NormalizedEmail = "operator1@test.com",
            EmailConfirmed = true,
        };

        await _userManager.CreateAsync(operatorUser1, "YXo1?5K=:u}s");

        await _userManager.AddClaimAsync(operatorUser1, new Claim(CustomClaims.Role, Roles.Operator.ToString()));

        ApplicationUser operatorUser2 = new()
        {
            TenantId = tenantIds[1],
            UserName = "operator2",
            NormalizedUserName = "operator2",
            Email = "operator2@test.com",
            NormalizedEmail = "operator2@test.com",
            EmailConfirmed = true,
        };

        await _userManager.CreateAsync(operatorUser2, "YXo1?5K=:u}s");

        await _userManager.AddClaimAsync(operatorUser2, new Claim(CustomClaims.Role, Roles.Operator.ToString()));

        ApplicationUser admin = new()
        {
            TenantId = tenantIds[0],
            UserName = "admin",
            NormalizedUserName = "admin",
            Email = "admin@test.com",
            NormalizedEmail = "admin@test.com",
            EmailConfirmed = true,
        };

        await _userManager.CreateAsync(admin, "YXo1?5K=:u}s");

        await _userManager.AddClaimAsync(admin, new Claim(CustomClaims.Role, Roles.Admin.ToString()));
    }
}
