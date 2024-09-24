using CineControl.CinemaService.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineControl.CinemaService.API.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly CinemaContext _context;

        public CinemaService(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cinema>> GetAllCinemas()
        {
            return await _context.Cinemas.Include(c => c.Theaters).ThenInclude(t => t.Seats).ToListAsync();
        }

        public async Task<Cinema> GetCinemaById(int id)
        {
            return await _context.Cinemas.Include(c => c.Theaters).ThenInclude(t => t.Seats).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCinema(string name, string address, string city, string state, string zipCode, List<TheaterConfig> theaterConfigs)
        {
            var cinema = CinemaFactory.CreateCinema(name, address, city, state, zipCode, theaterConfigs);
            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCinema(Cinema cinema)
        {
            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCinema(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            if (cinema != null)
            {
                _context.Cinemas.Remove(cinema);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Theater>> GetTheatersByCinemaId(int cinemaId)
        {
            var cinema = await _context.Cinemas.Include(c => c.Theaters).ThenInclude(t => t.Seats).FirstOrDefaultAsync(c => c.Id == cinemaId);
            return cinema?.Theaters ?? new List<Theater>();
        }

        public async Task AddTheater(int cinemaId, Theater theater)
        {
            var cinema = await _context.Cinemas.FindAsync(cinemaId);
            if (cinema != null)
            {
                theater.Id = cinema.Theaters.Count > 0 ? cinema.Theaters.Max(t => t.Id) + 1 : 1;
                cinema.Theaters.Add(theater);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTheater(int cinemaId, int theaterId)
        {
            var cinema = await _context.Cinemas.Include(c => c.Theaters).FirstOrDefaultAsync(c => c.Id == cinemaId);
            var theater = cinema?.Theaters.FirstOrDefault(t => t.Id == theaterId);
            if (theater != null)
            {
                cinema.Theaters.Remove(theater);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Seat>> GetSeatsByTheaterId(int theaterId)
        {
            var theater = await _context.Theaters.Include(t => t.Seats).FirstOrDefaultAsync(t => t.Id == theaterId);
            return theater?.Seats ?? new List<Seat>();
        }

        public async Task AddSeat(int theaterId, Seat seat)
        {
            var theater = await _context.Theaters.Include(t => t.Seats).FirstOrDefaultAsync(t => t.Id == theaterId);
            if (theater != null)
            {
                seat.Id = theater.Seats.Count > 0 ? theater.Seats.Max(s => s.Id) + 1 : 1;
                theater.Seats.Add(seat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveSeat(int theaterId, int seatId)
        {
            var theater = await _context.Theaters.Include(t => t.Seats).FirstOrDefaultAsync(t => t.Id == theaterId);
            var seat = theater?.Seats.FirstOrDefault(s => s.Id == seatId);
            if (seat != null)
            {
                theater.Seats.Remove(seat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
