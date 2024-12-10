using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;
using CineControl.CinemaService.API.Models.DTOs;

namespace CineControl.CinemaService.API.Service
{
    public class CinemaService : ICinemaService
    {
        private readonly CinemaContext _context;
        private readonly ITenantProvider _tenantProvider;

        public CinemaService(CinemaContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<ResultT<IEnumerable<CinemaResponse>>> GetAllCinemas()
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinemas = await _context.Cinemas
                .Include(c => c.Theaters)
                .ThenInclude(t => t.Seats)
                .ToListAsync();

            return cinemas.ToResponse();
        }

        public async Task<ResultT<CinemaResponse>> GetCinemaById(int id)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .ThenInclude(t => t.Seats)
                .FirstOrDefaultAsync(c => c.Id == id);

            return cinema is not null
                ? cinema.ToResponse()
                : CinemaErrors.CinemaNotFound(cinema.Id);
        }

        public async Task<ResultT<CitiesResponse>> GetAllCities()
        {
            var cities = await _context.Cinemas
                .Select(c => c.City)
                .Distinct()
                .ToListAsync();

            return cities.ToResponse();
        }

        public async Task<ResultT<IEnumerable<CinemaResponse>>> GetCinemasByCity(string city)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinemas = await _context.Cinemas
                .Where(c => c.City.ToLower() == city.ToLower())
                .ToListAsync();

            return cinemas.ToResponse();
        }

        public async Task<ResultT<CinemaResponse>> AddCinema(AddCinemaRequest request)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = CinemaFactory.CreateCinema(_tenantProvider.TenantId, request);
            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return cinema.ToResponse();
        }

        public async Task<Result> UpdateCinema(Cinema cinema)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var existing = await _context.Cinemas
                .FirstOrDefaultAsync(c => c.Id == cinema.Id);

            if (existing is null)
            {
                return CinemaErrors.CinemaNotFound(cinema.Id);
            }

            existing.Name = cinema.Name;
            existing.Address = cinema.Address;
            existing.City = cinema.City;
            existing.State = cinema.State;
            existing.ZipCode = cinema.ZipCode;

            _context.Cinemas.Update(existing);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteCinema(int id)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = await _context.Cinemas
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cinema is null)
            {
                return CinemaErrors.CinemaNotFound(id);
            }

            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<ResultT<Theater>> GetTheaterById(int theaterId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            return theater is not null
                ? theater
                : CinemaErrors.TheaterNotFound(theaterId);
        }

        public async Task<ResultT<IEnumerable<Theater>>> GetTheatersByCinemaId(int cinemaId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .ThenInclude(t => t.Seats)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            return cinema is not null
                ? cinema.Theaters
                : CinemaErrors.CinemaNotFound(cinemaId);
        }

        public async Task<Result> AddTheater(int cinemaId, AddTheaterRequest request)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            if (cinema is null)
            {
                return CinemaErrors.CinemaNotFound(cinemaId);
            }

            var theater = new Theater
            {
                TenantId = _tenantProvider.TenantId,
                Name = request.Name,
                SeatingCapacity = request.SeatingCapacity,
                Seats = CinemaFactory.GenerateSeats(_tenantProvider.TenantId, request.SeatingCapacity, request.SeatsPerRow)
            };

            cinema.Theaters.Add(theater);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> RemoveTheater(int cinemaId, int theaterId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            if (cinema is null)
            {
                return CinemaErrors.CinemaNotFound(cinemaId);
            }

            var theater = cinema.Theaters.FirstOrDefault(t => t.Id == theaterId);
            if (theater == null)
            {
                return CinemaErrors.TheaterNotFound(theaterId);
            }

            cinema.Theaters.Remove(theater);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<ResultT<IEnumerable<Seat>>> GetSeatsByTheaterId(int theaterId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            return theater is not null
                ? theater.Seats
                : CinemaErrors.TheaterNotFound(theaterId);
        }

        public async Task<Result> AddSeat(int theaterId, Seat seat)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            if (theater is null)
            {
                return CinemaErrors.TheaterNotFound(theaterId);
            }

            seat.TenantId = _tenantProvider.TenantId;
            seat.Id = theater.Seats.Any() ? theater.Seats.Max(s => s.Id) + 1 : 1;
            theater.Seats.Add(seat);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> RemoveSeat(int theaterId, int seatId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            if (theater is null)
            {
                return CinemaErrors.TheaterNotFound(theaterId);
            }

            var seat = theater.Seats.FirstOrDefault(s => s.Id == seatId);
            if (seat == null)
            {
                return CinemaErrors.SeatNotFound(seatId);
            }

            theater.Seats.Remove(seat);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
