using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models.DTOs.Movie;

public class AddMovieRequest
{
    [Required]
    [Length(1, 30, ErrorMessage = "Title must be between 1 and 30 characters")]
    public string Title { get; set; }
    [MaxLength(500,ErrorMessage = "Description cannot be longer than 500 characters")]
    public string Description { get; set; }
    [Required]
    [DisplayName("Short description")]
    [MaxLength(100,ErrorMessage = "Short description cannot be longer than 100 characters")]
    public string ShortDescription { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    [DisplayName("Duration in minutes")]
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Duration must be a positive number")]
    public int Duration { get; set; } // W minutach
    public IFormFile PosterFile { get; set; }
    public IFormFile PanoramicPosterFile { get; set; }
    public string Genre { get; set; }
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
    public double Rating { get; set; }
}
