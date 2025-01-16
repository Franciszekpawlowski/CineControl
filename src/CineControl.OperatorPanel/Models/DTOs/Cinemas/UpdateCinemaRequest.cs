using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models.DTOs.Cinemas;

public class UpdateCinemaRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(25)]
    public string Name { get; set; }
    [Required]
    [StringLength(25)]
    public string Address { get; set; }
    [Required]
    [StringLength(25)]
    public string City { get; set; } 
    [Required]
    [StringLength(25)]
    public string State { get; set; }
    [Required]
    [StringLength(25)]
    [RegularExpression(@"^\d{2}(-\d{3})?$", ErrorMessage = "Invalid Zip")]
    public string ZipCode { get; set; }

}
