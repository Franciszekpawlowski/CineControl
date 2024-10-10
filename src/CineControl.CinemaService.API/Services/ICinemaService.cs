using CineControl.CinemaService.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineControl.CinemaService.API.Services
{
    public interface ICinemaService
    {
        Task<IEnumerable<Cinema>> GetAllCinemas();
        Task<Cinema> GetCinemaById(int id);
        Task AddCinema(Cinema cinema);
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
