using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
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

        public async Task<ResultT<GetCinemasByCityResponse>> GetAllCities()
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
    }
}
