namespace BookingService.API.Models.Response
{
    public class ReservationResponse
    {
        public int ReservationId { get; set; }
        public int SeanceId { get; set; }
        public List<int> SeatIds { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
