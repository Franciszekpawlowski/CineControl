namespace CineControl.CinemaService.API.Models.DTOs.Seats;

public class SeatsResponse
{
    public IEnumerable<SeatResponse> Seats { get; set; } = new List<SeatResponse>();
}
