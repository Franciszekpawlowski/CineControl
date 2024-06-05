namespace CineControl.SeanceService.API.Models
{
    public class Seance
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
