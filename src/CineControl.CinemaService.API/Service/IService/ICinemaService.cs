using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Service.IService
{
    public interface ICinemaService
    {
        Task<ResultT<IEnumerable<Cinema>>> GetAllCinemas();
        Task<ResultT<Cinema>> GetCinemaById(int id);
        Task<ResultT<IEnumerable<string>>> GetAllCities();
        Task<ResultT<IEnumerable<Cinema>>> GetCinemasByCity(string city);

        Task<ResultT<Cinema>> AddCinema(AddCinemaRequest request);
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
