using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs.Seances;

namespace CineControl.SeanceService.API.Models.DTOs
{
    public static class SeanceMappingExtensions
    {
        public static SeanceDto ToDto(this Seance seance)
        {
            return new SeanceDto
            {
                Id = seance.Id,
                MovieId = seance.MovieId,
                MovieTitle = seance.Movie?.Title ?? string.Empty,
                TheaterId = seance.TheaterId,
                CinemaId = seance.CinemaId,
                PosterUrl = seance.Movie?.PosterUrl ?? string.Empty,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime
            };
        }
    }
}