namespace CineControl.CinemaService.API.Models.DTOs.Theaters;

public class AddTheaterRequest
{
    public string Name { get; set; } = "Default Theater";
    public int SeatingCapacity { get; set; } = 75;
    public int SeatsPerRow { get; set; } = 15;
}
