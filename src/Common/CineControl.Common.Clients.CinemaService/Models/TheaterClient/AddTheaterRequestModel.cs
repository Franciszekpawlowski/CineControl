namespace CineControl.Common.Clients.CinemaService.Models.TheaterClient;

public class AddTheaterRequestModel
{
    public string Name { get; set; }
    public int SeatingCapacity { get; set; }
    public int SeatsPerRow { get; set; }
}