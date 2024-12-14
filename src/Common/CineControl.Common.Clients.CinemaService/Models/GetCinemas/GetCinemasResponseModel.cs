namespace CineControl.Common.Clients.CinemaService.Models.GetCinemas;

public class GetCinemasResponseModel
{   
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class Theater 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SeatingCapacity { get; set; }
    public List<Seat> Seats { get; set; }
}

public class Seat
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatType Type { get; set; }
}

public enum SeatType
{
    Standard,
    VIP,
    Disabled
}