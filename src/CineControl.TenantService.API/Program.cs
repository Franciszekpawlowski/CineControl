using CineControl.TenantService.API.Data;
using CineControl.TenantService.API.Service;
using CineControl.TenantService.API.Service.IService;
using CineControl.Common.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TenantDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITenantService, TenantService>();

// Dodanie jwt, autoryzacji, loggera i swaggerów z CineControl.Common
builder.AddServiceDefaults("CineControl.TenantService.API");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Migracje bazy danych
app.applyMigrations();

app.MapControllers();

app.Run();
