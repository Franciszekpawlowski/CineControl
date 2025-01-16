using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Seats;
using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Service.IService;

public interface ISeatService
{
    Task<ResultT<IEnumerable<SeatResponse>>> GetSeatsByTheaterId(int cinemaId,int theaterId);    
}
