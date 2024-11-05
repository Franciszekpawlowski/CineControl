using CineControl.IdentityService.API.Data;
using Microsoft.EntityFrameworkCore;
using CineControl.IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using CineControl.IdentityService.API.Service.IService;
using CineControl.IdentityService.API.Service;
using CineControl.Common.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<appdbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<appdbContext>();

builder.AddServiceDefaults("CineControl.IdentityService.API");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.applyMigrations();

app.Run();