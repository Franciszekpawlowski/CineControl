using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.Request.Cinemas;

namespace CineControl.CinemaService.API.Services
{
    public interface ICinemaService
    {
        Task<IEnumerable<Cinema>> GetAllCinemas();
        Task<Cinema?> GetCinemaById(int id);
        Task AddCinema(AddCinemaRequest request);
        Task UpdateCinema(Cinema cinema);
        Task DeleteCinema(int id);

        Task<IEnumerable<Theater>> GetTheatersByCinemaId(int cinemaId);
        Task AddTheater(int cinemaId, Theater theater);
        Task RemoveTheater(int cinemaId, int theaterId);

        Task<IEnumerable<Seat>> GetSeatsByTheaterId(int theaterId);
        Task AddSeat(int theaterId, Seat seat);
        Task RemoveSeat(int theaterId, int seatId);
    }
}
