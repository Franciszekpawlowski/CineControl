namespace CineControl.CinemaService.API.Models
{
    public class TheaterConfig
    {
        public string Name { get; set; }
        public int SeatingCapacity { get; set; } = 75;
        public int SeatsPerRow { get; set; } = 15;
    }
}
