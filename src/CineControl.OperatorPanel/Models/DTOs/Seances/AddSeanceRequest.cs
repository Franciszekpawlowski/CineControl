namespace CineControl.OperatorPanel.Models.DTOs.Seances;

public class AddSeanceRequest
{
    public int MovieId { get; set; }
    public int TheaterId { get; set; }
    public int CinemaId { get; set; }
    public DateTime StartTime { get; set; }

}