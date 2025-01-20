using CineControl.CinemaService.API.Models.DTOs.Seats;

namespace CineControl.CinemaService.API.Models.DTOs.Theaters;

public static class TheaterMappingExtensions
{
    public static TheaterResponse ToResponse(this Theater theater)
    {
        return new TheaterResponse
        {
            Id = theater.Id,
            Name = theater.Name,
            SeatingCapacity = theater.SeatingCapacity,
            SeatsPerRow = theater.SeatsPerRow
            // Seats = theater.Seats.Select(s => s.ToResponse()).ToList()
        };
    }

    public static List<TheaterResponse> ToResponse(this ICollection<Theater> theaters)
    {
        return theaters.Select(ToResponse).ToList();
    }
}
