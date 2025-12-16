using CineControl.BookingService.API.Models;
using CineControl.BookingService.API.Models.DTOs.Reservations;

namespace BookingService.API.Models.DTOs
{
    public static class ReservationMappingExtensions
    {
        public static ReservationResponse ToResponse(this Reservation reservation)
        {
            return new ReservationResponse
            {
                ReservationId = reservation.Id,
                SeanceId = reservation.SeanceId,
                SeatIds = reservation.Tickets.Select(t => t.SeatId).ToList(),
                ReservationTime = reservation.ReservationTime
            };
        }
    }
}
