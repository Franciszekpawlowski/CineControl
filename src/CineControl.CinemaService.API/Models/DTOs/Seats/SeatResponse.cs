namespace CineControl.CinemaService.API.Models.DTOs.Seats;

public class SeatResponse
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Number { get; set; }
    public string Type { get; set; } = string.Empty;
}
