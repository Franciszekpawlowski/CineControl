using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Service;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.ServiceDefaults;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja usług
builder.AddServiceDefaults("CineControl.CinemaService.API");
builder.Services.AddControllers();
builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddTenantProvider();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfiguracja middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// **Przestawienie UseTenantMiddleware wyżej**
app.UseTenantMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.ApplyMigrations();
app.MapControllers();

app.Run();
