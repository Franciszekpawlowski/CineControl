using System.Collections.Generic;

namespace CineControl.BookingService.API.Models.DTOs.Reservations
{
    public class ReservationRequest
    {
        public int SeanceId { get; set; }
        public List<int> SeatIds { get; set; }
    }
}
