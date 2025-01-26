using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CineControl.SeanceService.API.Models;

namespace CineControl.SeanceService.API.Data
{
    public static class MigrationsExtension
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (db.Database.GetPendingMigrations().Any())
                {
                    db.Database.Migrate();
                }

                if (!db.Movies.Any())
                {
                    SeedMovies(db);
                }

                if (!db.Seances.Any())
                {
                    SeedSeances(db);
                }
            }
            return app;
        }


        private static void SeedMovies(AppDbContext db)
        {
            var tenantIds = new[]
            {
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Guid.Parse("e647f619-447d-40f0-b0c6-b1a4eb17d7db")
            };

            var now = DateTime.UtcNow.Date;

            var pastMovies = new List<Movie>();
            var futureMovies = new List<Movie>();
            
            foreach (Guid tenantid in tenantIds)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var releaseDate = now.AddDays(-14 + i);
                    pastMovies.Add(new Movie
                    {
                        Title = $"Past Movie #{i}",
                        Description = $"Opis filmu (premiera {releaseDate:yyyy-MM-dd})",
                        ShortDescription = $"Krótki opis {i}",
                        ReleaseDate = releaseDate,
                        Duration = 120,
                        PosterUrl = "https://example.com/poster.jpg",
                        PosterB64 = "base64-encoded-poster-data",
                        PanoramicPosterUrl = "https://example.com/panorama.jpg",
                        PanoramicPosterB64 = "base64-encoded-panorama-data",
                        Genre = "Action",
                        Rating = 4.5
                    });
                }

                for (int i = 1; i <= 5; i++)
                {
                    var releaseDate = now.AddDays(i);
                    futureMovies.Add(new Movie
                    {
                        Title = $"Future Movie #{i}",
                        Description = $"Opis filmu (premiera {releaseDate:yyyy-MM-dd})",
                        ShortDescription = $"Krótki opis {i}",
                        ReleaseDate = releaseDate,
                        Duration = 100,
                        PosterUrl = "https://example.com/poster-future.jpg",
                        PosterB64 = "base64-encoded-poster-data",
                        PanoramicPosterUrl = "https://example.com/panorama-future.jpg",
                        PanoramicPosterB64 = "base64-encoded-panorama-data",
                        Genre = "Comedy",
                        Rating = 4.2
                    });
                }
            };
            
            var allMoviesToSeed = futureMovies.Concat(pastMovies).ToList();

            // foreach (var tenantId in tenantIds)
            // {
                
            //     foreach (var pm in pastMovies)
            //     {
            //         allMoviesToSeed.Add(new Movie
            //         {
            //             TenantID = tenantId,
            //             Title = pm.Title,
            //             Description = pm.Description,
            //             ShortDescription = pm.ShortDescription,
            //             ReleaseDate = pm.ReleaseDate,
            //             Duration = pm.Duration,
            //             PosterUrl = pm.PosterUrl,
            //             PanoramicPosterUrl = pm.PanoramicPosterUrl,
            //             Genre = pm.Genre,
            //             Rating = pm.Rating
            //         });
            //     }

                
            //     foreach (var fm in futureMovies)
            //     {
            //         allMoviesToSeed.Add(new Movie
            //         {
            //             TenantID = tenantId,
            //             Title = fm.Title,
            //             Description = fm.Description,
            //             ShortDescription = fm.ShortDescription,
            //             ReleaseDate = fm.ReleaseDate,
            //             Duration = fm.Duration,
            //             PosterUrl = fm.PosterUrl,
            //             PanoramicPosterUrl = fm.PanoramicPosterUrl,
            //             Genre = fm.Genre,
            //             Rating = fm.Rating
            //         });
            //     }
            // }

            db.Movies.AddRange(allMoviesToSeed);
            db.SaveChanges();
        }


        private static void SeedSeances(AppDbContext db)
        {
            var tenantIds = new[]
            {
                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Guid.Parse("e647f619-447d-40f0-b0c6-b1a4eb17d7db")
            };

            var nowDate = DateTime.UtcNow.Date;

            var allReleasedMovies = db.Movies
                .IgnoreQueryFilters()
                .Where(m => m.ReleaseDate <= nowDate)
                .ToList();

            
            var tenantCinemaMap = new Dictionary<Guid, IEnumerable<int>>
            {
                [tenantIds[0]] = Enumerable.Range(1, 5),  
                [tenantIds[1]] = Enumerable.Range(6, 5)  
            };

            
            var dailyStartTimes = new[]
            {
                new TimeSpan(9, 0, 0),
                new TimeSpan(12, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(18, 0, 0),
                new TimeSpan(21, 0, 0)
            };

            var daysRange = Enumerable.Range(-1, 6)
                .Select(offset => nowDate.AddDays(offset))
                .ToList();

            var seancesToSeed = new List<Seance>();
            var random = new Random(); 

            foreach (var tenantId in tenantIds)
            {
                
                var tenantMovies = allReleasedMovies
                    .Where(m => m.TenantID == tenantId)
                    .ToList();

                var cinemaIds = tenantCinemaMap[tenantId];

                foreach (var cinemaId in cinemaIds)
                {

                    var theaterId = (cinemaId - 1) * 3 + 1;  

                    foreach (var day in daysRange)
                    {
                        
                        foreach (var timeSpan in dailyStartTimes)
                        {
                            
                            if (tenantMovies.Any())
                            {
                                var movieIndex = random.Next(tenantMovies.Count);
                                var chosenMovie = tenantMovies[movieIndex];

                                var start = day.Add(timeSpan);
                                var end   = start.AddMinutes(chosenMovie.Duration);

                                seancesToSeed.Add(new Seance
                                {
                                    TenantId  = tenantId,
                                    CinemaId  = cinemaId,
                                    TheaterId = theaterId,
                                    MovieId   = chosenMovie.Id,
                                    StartTime = start,
                                    EndTime   = end
                                });
                            }
                        }
                    }
                }
            }

            db.Seances.AddRange(seancesToSeed);
            db.SaveChanges();
        }


    }
}
