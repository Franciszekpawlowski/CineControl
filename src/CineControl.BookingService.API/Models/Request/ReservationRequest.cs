namespace BookingService.API.Models.Request
{
    public class ReservationRequest
    {
        public int SeanceId { get; set; }
        public List<int> SeatIds { get; set; }
    }
}
