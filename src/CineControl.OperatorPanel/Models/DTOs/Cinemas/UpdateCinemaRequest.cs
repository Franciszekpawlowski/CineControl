using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models.DTOs.Cinemas;

public class UpdateCinemaRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(25, ErrorMessage = "Cinema Name must be between 3 and 25 characters", MinimumLength = 3)]
    [DisplayName("Cinema Name")]
    public string Name { get; set; }
    [Required]
    [StringLength(25, ErrorMessage = "Address must be between 3 and 25 characters", MinimumLength = 3)]
    public string Address { get; set; }
    [Required]
    [StringLength(25, ErrorMessage = "City must be between 3 and 25 characters", MinimumLength = 3)]
    public string City { get; set; } 
    [Required]
    [StringLength(25, ErrorMessage = "State must be between 3 and 25 characters", MinimumLength = 3)]
    public string State { get; set; }
    [Required]
    [StringLength(5)]
    [RegularExpression(@"^\d{2}(-\d{3})?$", ErrorMessage = "Invalid Zip")]
    public string ZipCode { get; set; }

}
