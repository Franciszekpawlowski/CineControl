namespace BookingService.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SeanceId { get; set; }
        public DateTime ReservationTime { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
