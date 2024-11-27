namespace BookingService.API.Models
{
    public class SeanceDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
