namespace CineControl.CinemaService.API.Models.DTOs.Theaters;

public class TheatersResponse
{
    public IEnumerable<TheaterResponse> Theaters { get; set; } = new List<TheaterResponse>();
}
