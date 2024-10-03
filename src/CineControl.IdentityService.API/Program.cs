using CineControl.IdentityService.API.Data;
using Microsoft.EntityFrameworkCore;
using CineControl.IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using CineControl.IdentityService.API.Service.IService;
using CineControl.IdentityService.API.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<appdbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<appdbContext>().AddDefaultTokenProviders();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator,JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ApplyMigrations();

app.Run();


void ApplyMigrations()
{
    using(var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<appdbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}