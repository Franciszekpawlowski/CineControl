using BookingService.API.Models;
using BookingService.API.Models.Request;
using BookingService.API.Models.Response;
using BookingService.API.Models.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingService.API.Services
{
    public interface IReservationService
    {
        Task<bool> AreSeatsAvailableAsync(int seanceId, List<int> seatIds);
        Task<GenericResults<ReservationResponse>> CreateReservationAsync(ReservationRequest request);
        Task<List<int>> GetReservedSeatsAsync(int seanceId);
    }
}
