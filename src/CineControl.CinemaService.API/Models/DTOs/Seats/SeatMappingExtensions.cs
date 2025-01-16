namespace CineControl.CinemaService.API.Models.DTOs.Seats;

public static class SeatMappingExtensions
{
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

    public static List<SeatResponse> ToResponse(this ICollection<Seat> seats)
    {
        return seats.Select(ToResponse).ToList();
    }
}
