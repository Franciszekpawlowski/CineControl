using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Seats;
using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Service.IService;

public interface ISeatService
{
    Task<ResultT<IEnumerable<SeatResponse>>> GetSeatsByTheaterId(int theaterId);
    Task<Result> AddSeat(int theaterId, Seat seat);
    Task<Result> RemoveSeat(int theaterId, int seatId);
    
}
