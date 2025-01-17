namespace CineControl.Common.Clients.CinemaService.Models.TheaterClient;

public class GetTheaterResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int SeatingCapacity { get; set; }
}