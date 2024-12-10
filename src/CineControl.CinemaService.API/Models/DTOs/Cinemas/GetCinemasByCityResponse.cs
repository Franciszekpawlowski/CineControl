namespace CineControl.CinemaService.API.Models.DTOs.Cinemas;

public class GetCinemasByCityResponse
{
    public IEnumerable<string> Cities { get; set; } = Enumerable.Empty<string>();
}
