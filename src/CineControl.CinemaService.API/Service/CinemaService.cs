using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public async Task<ResultT<IEnumerable<Cinema>>> GetAllCinemas()
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinemas = await _context.Cinemas
                .Include(c => c.Theaters)
                .ThenInclude(t => t.Seats)
                .ToListAsync();

            return cinemas;
        }

        public async Task<ResultT<Cinema>> GetCinemaById(int id)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .ThenInclude(t => t.Seats)
                .FirstOrDefaultAsync(c => c.Id == id);

            return cinema is not null
                ? cinema
                : CinemaErrors.NotFound($"Cinema with id {id} not found");
        }

        public async Task<ResultT<IEnumerable<string>>> GetAllCities()
        {
            var cities = await _context.Cinemas
                .Select(c => c.City)
                .Distinct()
                .ToListAsync();

            return cities;
        }

        public async Task<ResultT<IEnumerable<Cinema>>> GetCinemasByCity(string city)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinemas = await _context.Cinemas
                .Where(c => c.City.ToLower() == city.ToLower())
                .ToListAsync();

            return cinemas;
        }

        public async Task<ResultT<Cinema>> AddCinema(AddCinemaRequest request)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinema = CinemaFactory.CreateCinema(_tenantProvider.TenantId, request);
            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return cinema;
        }

        public async Task<Result> UpdateCinema(Cinema cinema)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var existing = await _context.Cinemas
                .FirstOrDefaultAsync(c => c.Id == cinema.Id);

            if (existing is null)
            {
                return CinemaErrors.NotFound($"Cinema with id {cinema.Id} not found");
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
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinema = await _context.Cinemas
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cinema is null)
            {
                return CinemaErrors.NotFound($"Cinema with id {id} not found");
            }

            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<ResultT<Theater>> GetTheaterById(int theaterId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            return theater is not null
                ? theater
                : CinemaErrors.NotFound($"Theater with id {theaterId} not found");
        }

        public async Task<ResultT<IEnumerable<Theater>>> GetTheatersByCinemaId(int cinemaId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .ThenInclude(t => t.Seats)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            return cinema is not null
                ? cinema.Theaters
                : CinemaErrors.NotFound($"Cinema with id {cinemaId} not found");
        }

        public async Task<Result> AddTheater(int cinemaId, AddTheaterRequest request)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            if (cinema is null)
            {
                return CinemaErrors.NotFound($"Cinema with id {cinemaId} not found");
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
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var cinema = await _context.Cinemas
                .Include(c => c.Theaters)
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            if (cinema is null)
            {
                return CinemaErrors.NotFound($"Cinema with id {cinemaId} not found");
            }

            var theater = cinema.Theaters.FirstOrDefault(t => t.Id == theaterId);
            if (theater == null)
            {
                return CinemaErrors.NotFound($"Theater with id {theaterId} not found");
            }

            cinema.Theaters.Remove(theater);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<ResultT<IEnumerable<Seat>>> GetSeatsByTheaterId(int theaterId)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            return theater is not null
                ? theater.Seats
                : CinemaErrors.NotFound($"Seat with id {theaterId} not found");
        }

        public async Task<Result> AddSeat(int theaterId, Seat seat)
        {
            if (!_tenantProvider.HasTenant)
            {
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            if (theater is null)
            {
                return CinemaErrors.NotFound($"Theater with id {theaterId} not found");
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
                return CinemaErrors.AccessUnauthorized("No tenant specified");
            }

            var theater = await _context.Theaters
                .Include(t => t.Seats)
                .FirstOrDefaultAsync(t => t.Id == theaterId);

            if (theater is null)
            {
                return CinemaErrors.NotFound($"Seat with id {seatId} not found");
            }

            var seat = theater.Seats.FirstOrDefault(s => s.Id == seatId);
            if (seat == null)
            {
                return CinemaErrors.NotFound($"Seat with id {seatId} not found");
            }

            theater.Seats.Remove(seat);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
