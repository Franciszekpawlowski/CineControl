namespace CineControl.CinemaService.API.Models
{
    public class Theater
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public int CinemaId { get; set;}
        public Cinema Cinema{ get; set; } = null!;

        public int SeatingCapacity => Seats.Count;

        public int SeatsPerRow => Seats.Select(s => s.Row).Distinct().Count();
    }
}
