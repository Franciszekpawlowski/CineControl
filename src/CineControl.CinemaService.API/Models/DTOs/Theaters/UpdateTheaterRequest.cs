namespace CineControl.CinemaService.API.Models.DTOs.Theaters;

public class UpdateTheaterRequest
{
    public string Name { get; set; }
    public int SeatingCapacity { get; set; }
    public int SeatsPerRow { get; set; }

}