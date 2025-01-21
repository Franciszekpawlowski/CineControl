using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models.DTOs.Movie;

public class UpdateMovieRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public int Duration { get; set; } // W minutach
    public string PosterUrl { get; set; }
    public string PanoramicPosterUrl { get; set; }
    public string Genre { get; set; }
    public double Rating { get; set; }
}