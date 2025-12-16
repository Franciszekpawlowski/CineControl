using Microsoft.EntityFrameworkCore;
using CineControl.CinemaService.API.Models;

namespace CineControl.CinemaService.API.Data
{
    public static class MigrationsExtension
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        { 
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<CinemaContext>();
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
                if (!_db.Cinemas.Any())
                {
                    SeedData(_db);
                }
            }
            
            return app;
        }
        private static void SeedData(CinemaContext db)
        {
            var tenantIds = new[]
            {
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Guid.Parse("e647f619-447d-40f0-b0c6-b1a4eb17d7db")
            };

            var cities = new[] { "Warsaw", "Krakow", "Gdansk", "Wroclaw", "Poznan" };

            var cinemasToSeed = new List<Cinema>();

            foreach (var tenantId in tenantIds)
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    var city = cities[i];
                    var cinema = new Cinema
                    {
                        TenantId = tenantId,
                        Name     = $"Cinema {i+1} ({city})",
                        Address  = $"Test Street {i+1}",
                        City     = city,
                        State    = "N/A",
                        ZipCode  = $"00-0{i+1}"
                    };

                    for (int theaterNum = 1; theaterNum <= 3; theaterNum++)
                    {
                        var theater = new Theater
                        {
                            TenantId = tenantId,
                            Name     = $"Theater {theaterNum}",
                        };

                        for (int row = 1; row <= 10; row++)
                        {
                            for (int seatNumber = 1; seatNumber <= 10; seatNumber++)
                            {
                                var seat = new Seat
                                {
                                    TenantId = tenantId,
                                    Row      = row,
                                    Number   = seatNumber,
                                    Type     = SeatType.Standard
                                };
                                theater.Seats.Add(seat);
                            }
                        }

                        cinema.Theaters.Add(theater);
                    }

                    cinemasToSeed.Add(cinema);
                }
            }

            db.Cinemas.AddRange(cinemasToSeed);
            db.SaveChanges();
        }
    }
}
