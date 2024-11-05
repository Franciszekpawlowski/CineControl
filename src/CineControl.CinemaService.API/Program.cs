using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Services;
using CineControl.Common.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using CineControl.CinemaService.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICinemaService, CinemaService>();


builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
