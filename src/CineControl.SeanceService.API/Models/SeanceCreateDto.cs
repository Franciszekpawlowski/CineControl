namespace CineControl.SeanceService.API.Models
{
    public class SeanceCreateDto
    {
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
