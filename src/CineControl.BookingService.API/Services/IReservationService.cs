using CineControl.BookingService.API.Models.DTOs.Reservations;
using CineControl.Common.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineControl.BookingService.API.Services
{
    public interface IReservationService
    {
        Task<ResultT<ReservationResponse>> CreateReservationAsync(ReservationRequest request);
        Task<ResultT<List<int>>> GetReservedSeatsAsync(int seanceId);
        Task<bool> AreSeatsAvailableAsync(int seanceId, List<int> seatIds);
        Task<ResultT<List<ReservationResponse>>> GetUserReservationsAsync();
        Task<Result> CancelReservationAsync(int reservationId);
    }
}
