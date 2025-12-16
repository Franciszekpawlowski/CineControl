using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models.DTOs.Seances;

public class GetSeanceResponse
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    [Display(Name = "Movie Title")]
    public string MovieTitle { get; set; }
    public int TheaterId { get; set; }
    public int CinemaId { get; set; }
    [Display(Name = "Poster")]
    public string PosterUrl { get; set; }
    public string PosterBase64 { get; set; }
    [DataType(DataType.ImageUrl)]
    [Display(Name = "Poster")]
    public string PosterBase64Thumbnail { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}