using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.CinemaService.API.Models.DTOs.Seats;

namespace CineControl.CinemaService.API.Models.DTOs;

public static class CinemaMappingExtensions
{
    public static CinemaResponse ToResponse(this Cinema cinema)
    {
        return new CinemaResponse
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Address = cinema.Address,
            City = cinema.City,
            State = cinema.State,
            ZipCode = cinema.ZipCode,
            Theaters = cinema.Theaters.Select(t => t.ToResponse()).ToList()
        };
    }

    public static TheaterResponse ToResponse(this Theater theater)
    {
        return new TheaterResponse
        {
            Id = theater.Id,
            Name = theater.Name,
            SeatingCapacity = theater.SeatingCapacity,
            Seats = theater.Seats.Select(s => s.ToResponse()).ToList()
        };
    }

    public static SeatResponse ToResponse(this Seat seat)
    {
        return new SeatResponse
        {
            Id = seat.Id,
            Row = seat.Row,
            Number = seat.Number,
            Type = seat.Type.ToString()
        };
    }
}
