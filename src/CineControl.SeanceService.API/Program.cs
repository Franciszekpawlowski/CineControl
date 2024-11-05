using CineControl.Common.ServiceDefaults;
using CineControl.SeanceService.API.Data;
using Microsoft.EntityFrameworkCore;
using CineControl.SeanceService.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure DbContext with PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddHttpClient<ITheaterService, TheaterService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5112/");
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
