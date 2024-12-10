using CineControl.Common.ServiceDefaults;
using CineControl.Common.Tenant;
using CineControl.BookingService.API.Data;
using CineControl.BookingService.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodanie usług do kontenera DI
builder.Services.AddControllers();

// Konfiguracja PostgreSQL database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodanie wspólnych usług (JWT, autoryzacja, logowanie, multi-tenancy)
builder.AddServiceDefaults("CineControl.BookingService.API");

// Rejestracja serwisów
builder.Services.AddScoped<IReservationService, ReservationService>();

// Dodanie HttpClient, jeśli potrzebne
builder.Services.AddHttpClient();

builder.Services.AddTenantProvider();

// Budowanie aplikacji
var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseTenantMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Apply migrations
app.ApplyMigrations();

app.Run();
