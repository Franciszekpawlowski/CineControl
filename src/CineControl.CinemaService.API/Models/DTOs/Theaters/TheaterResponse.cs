using CineControl.CinemaService.API.Models.DTOs.Seats;
namespace CineControl.CinemaService.API.Models.DTOs.Theaters;

public class TheaterResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeatingCapacity { get; set; }
    // public List<SeatResponse> Seats { get; set; } = [];
}
