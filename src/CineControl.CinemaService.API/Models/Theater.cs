namespace CineControl.CinemaService.API.Models
{
    public class Theater
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
    }
}
