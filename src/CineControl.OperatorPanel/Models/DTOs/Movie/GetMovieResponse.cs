namespace CineControl.OperatorPanel.Models.DTOs.Movie;

public class GetMovieResponse()
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Duration { get; set; } // W minutach
    public string PosterUrl { get; set; }
    public string PanoramicPosterUrl { get; set; }
    public string Genre { get; set; }
    public double Rating { get; set; }
}