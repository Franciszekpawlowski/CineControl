using System.ComponentModel;

namespace CineControl.OperatorPanel.Models.DTOs.Cinemas;

public class GetCinemaResponse()
{
    [DisplayName("Id")]
    public int Id { get; set; }
    [DisplayName("Name")]
    public string Name { get; set; }
    [DisplayName("Address")]
    public string Address { get; set; }
    [DisplayName("City")]
    public string City { get; set; }
    [DisplayName("State")]
    public string State { get; set; }
    [DisplayName("Zip Code")]
    public string ZipCode { get; set; }
}