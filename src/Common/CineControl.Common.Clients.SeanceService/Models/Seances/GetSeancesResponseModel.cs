namespace CineControl.Common.Clients.SeanceService.Models.Seances;

public class GetSeancesResponseModel
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public int TheaterId { get; set; }
    public int CinemaId { get; set; }
    public string PosterUrl { get; set; }
    public string PosterBase64 { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}