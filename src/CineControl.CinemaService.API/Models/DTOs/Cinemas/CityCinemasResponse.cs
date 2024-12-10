namespace CineControl.CinemaService.API.Models.DTOs.Cinemas;

public class CityCinemasResponse
{
     public Guid TenantId { get; set; }
    public string City { get; set; } = string.Empty;
    public IEnumerable<CinemaResponse> Cinemas { get; set; } = new List<CinemaResponse>();
}
