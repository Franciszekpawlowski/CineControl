namespace BookingService.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
