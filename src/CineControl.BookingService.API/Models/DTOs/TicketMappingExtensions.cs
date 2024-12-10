using CineControl.BookingService.API.Models;
using CineControl.BookingService.API.Models.DTOs.Seatings;

namespace BookingService.API.Models.DTOs
{
    public static class TicketMappingExtensions
    {
        public static ReservedSeatsResponse ToReservedSeatsResponse(this List<int> seatIds)
        {
            return new ReservedSeatsResponse
            {
                SeatIds = seatIds.ToArray()
            };
        }
    }
}
