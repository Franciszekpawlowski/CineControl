namespace CineControl.Common.Clients.CinemaService.Models.AddCinema;

public class AddCinemaRequestModel
{   
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public List<TheaterConfig> TheaterConfigs { get; set; }
}

public class TheaterConfig 
{
    public string Name { get; set; }
    public int SeatingCapacity { get; set; } = 75;
    public int SeatsPerRow { get; set; } = 15;
}