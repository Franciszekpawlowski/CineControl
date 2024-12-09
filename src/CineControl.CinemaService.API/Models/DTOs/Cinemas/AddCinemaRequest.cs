namespace CineControl.CinemaService.API.Models.DTOs.Cinemas;

public class AddCinemaRequest
{
     public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; } 
    public string State { get; set; }
    public string ZipCode { get; set; }
    public List<TheaterConfig> TheaterConfigs { get; set; } = new();
}
