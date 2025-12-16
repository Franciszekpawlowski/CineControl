namespace CineControl.CinemaService.API.Models.DTOs.Seats;

public class AddSeatRequest
{
    public int Row { get; set; }
    public int Number { get; set; }
    public string Type { get; set; } = "Standard";
}
