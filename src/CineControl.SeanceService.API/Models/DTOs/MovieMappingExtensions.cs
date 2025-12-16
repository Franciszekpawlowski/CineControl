using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs.Movies;

namespace CineControl.SeanceService.API.Models.DTOs
{
    public static class MovieMappingExtensions
    {
        public static MovieResponse ToResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ShortDescription = movie.ShortDescription,
                ReleaseDate = movie.ReleaseDate,
                Duration = movie.Duration,
                PosterUrl = movie.PosterUrl,
                PosterB64 = movie.PosterB64,
                PanoramicPosterUrl = movie.PanoramicPosterUrl,
                PanoramicPosterB64 = movie.PanoramicPosterB64,
                Genre = movie.Genre,
                Rating = movie.Rating
            };
        }
    }
}