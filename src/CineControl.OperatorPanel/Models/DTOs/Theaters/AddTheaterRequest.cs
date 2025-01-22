using System.ComponentModel.DataAnnotations;
using CineControl.OperatorPanel.Validators;

namespace CineControl.OperatorPanel.Models.DTOs.Theaters
{
    public class AddTheaterRequest
    {
        [Required]
        [Display(Name = "Theater Name")]
        [StringLength(50, ErrorMessage = "Theater name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Seating capacity must be between 1 and 100")]
        public int SeatingCapacity { get; set; }
        [Required]
        [SeatsPerRowCapacity]
        [Range(1, 10, ErrorMessage = "Seats per row must be between 1 and 10")]
        public int SeatsPerRow { get; set; }

    }
}
