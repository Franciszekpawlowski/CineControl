using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Service.IService;

public interface ITheaterServices
{
    Task<Result> RemoveTheater(int cinemaId, int theaterId);
    Task<ResultT<TheaterResponse>> GetTheaterById(int theaterId);
    Task<ResultT<IEnumerable<TheaterResponse>>> GetTheatersByCinemaId(int cinemaId);
    Task<Result> AddTheater(int cinemaId, AddTheaterRequest request);
}
