namespace CineControl.OperatorPanel.Models.DTOs.Seances;

public class GetSeanceResponse
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public int TheaterId { get; set; }
    public int CinemaId { get; set; }
    public string PosterUrl { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}