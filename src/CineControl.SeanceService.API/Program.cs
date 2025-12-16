using CineControl.Common.ServiceDefaults;
using CineControl.Common.Tenant;
using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Service;
using CineControl.SeanceService.API.Service.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodanie domyślnych usług
builder.AddServiceDefaults("CineControl.SeanceService.API");

// Dodanie kontrolerów
builder.Services.AddControllers();

// Dodanie DbContext z PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja serwisów
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ISeanceService, SeanceService>();

builder.Services.AddTenantProvider();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseTenantMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.ApplyMigrations();
app.MapControllers();

app.Run();
