using CineControl.IdentityService.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CineControl.IdentityService.API.Data
{
    public class appdbContext : IdentityDbContext<ApplicationUser>
    {
        public appdbContext(DbContextOptions<appdbContext> options) : base(options){}

        public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}