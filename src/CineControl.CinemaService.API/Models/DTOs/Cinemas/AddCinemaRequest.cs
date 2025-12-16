using System.ComponentModel.DataAnnotations;

namespace CineControl.CinemaService.API.Models.DTOs.Cinemas;

public class AddCinemaRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string City { get; set; } 
    [Required]
    public string State { get; set; }
    [Required]
    public string ZipCode { get; set; }
}
