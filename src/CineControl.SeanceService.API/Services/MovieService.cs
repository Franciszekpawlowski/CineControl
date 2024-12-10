using CineControl.Common.Results;
using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Models;
using CineControl.Common.Tenant;
using CineControl.SeanceService.API.Models.DTOs.Movies;
using CineControl.SeanceService.API.Service.IService;
using CineControl.SeanceService.API.Errors;
using Microsoft.EntityFrameworkCore;

namespace CineControl.SeanceService.API.Service
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public MovieService(AppDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<ResultT<IEnumerable<Movie>>> GetAllMovies()
        {
            if (!_tenantProvider.HasTenant)
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task<ResultT<Movie>> GetMovieById(int id)
        {
            if (!_tenantProvider.HasTenant)
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return movie != null
                ? movie
                : SeanceErrors.NotFound($"Movie with id {id} not found");
        }

        public async Task<ResultT<Movie>> AddMovie(AddMovieRequest request)
        {
            if (!_tenantProvider.HasTenant)
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var movie = new Movie
            {
                TenantID = _tenantProvider.TenantId,
                Title = request.Title,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                ReleaseDate = request.ReleaseDate,
                Duration = request.Duration,
                PosterUrl = request.PosterUrl,
                PanoramicPosterUrl = request.PanoramicPosterUrl,
                Genre = request.Genre,
                Rating = request.Rating
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Result> UpdateMovie(Movie movie)
        {
            if (!_tenantProvider.HasTenant)
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var existing = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (existing == null)
            {
                return SeanceErrors.NotFound($"Movie with id {movie.Id} not found");
            }

            // Opcjonalnie: Można sprawdzić, czy film należy do tego samego tenant
            // if (existing.TenantID != _tenantProvider.TenantId)
            // {
            //     return SeanceErrors.AccessUnauthorized("You do not have access to modify this movie.");
            // }

            existing.Title = movie.Title;
            existing.Description = movie.Description;
            existing.ShortDescription = movie.ShortDescription;
            existing.ReleaseDate = movie.ReleaseDate;
            existing.Duration = movie.Duration;
            existing.PosterUrl = movie.PosterUrl;
            existing.PanoramicPosterUrl = movie.PanoramicPosterUrl;
            existing.Genre = movie.Genre;
            existing.Rating = movie.Rating;

            _context.Movies.Update(existing);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteMovie(int id)
        {
            if (!_tenantProvider.HasTenant)
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return SeanceErrors.NotFound($"Movie with id {id} not found");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
