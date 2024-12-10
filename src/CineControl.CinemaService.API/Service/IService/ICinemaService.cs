using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Service.IService
{
    public interface ICinemaService
    {
        Task<ResultT<IEnumerable<CinemaResponse>>> GetAllCinemas();
        Task<ResultT<CinemaResponse>> GetCinemaById(int id);
        Task<ResultT<CitiesResponse>> GetAllCities();
        Task<ResultT<IEnumerable<CinemaResponse>>> GetCinemasByCity(string city);

        Task<ResultT<CinemaResponse>> AddCinema(AddCinemaRequest request);
        Task<Result> UpdateCinema(Cinema cinema);
        Task<Result> DeleteCinema(int id);

        Task<ResultT<Theater>> GetTheaterById(int theaterId);
        Task<ResultT<IEnumerable<Theater>>> GetTheatersByCinemaId(int cinemaId);

        Task<Result> AddTheater(int cinemaId, AddTheaterRequest request);
        Task<Result> RemoveTheater(int cinemaId, int theaterId);

        Task<ResultT<IEnumerable<Seat>>> GetSeatsByTheaterId(int theaterId);
        Task<Result> AddSeat(int theaterId, Seat seat);
        Task<Result> RemoveSeat(int theaterId, int seatId);
    }
}
