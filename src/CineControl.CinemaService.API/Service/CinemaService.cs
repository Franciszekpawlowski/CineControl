using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CineControl.CinemaService.API.Service
{
    public class CinemaService(CinemaContext context, ITenantProvider tenantProvider) : ICinemaService
    {
        private readonly CinemaContext _context = context;
        private readonly ITenantProvider _tenantProvider = tenantProvider;

        public async Task<ResultT<IEnumerable<CinemaResponse>>> GetAllCinemas()
        {
            if (!_tenantProvider.HasTenant())
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinemas = await _context.Cinemas
                .ToListAsync();

            return cinemas.ToResponse();
        }

        public async Task<ResultT<CinemaResponse>> GetCinemaById(int id)
        {
            if (!_tenantProvider.HasTenant())
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinema = await _context.Cinemas
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
            if (!_tenantProvider.HasTenant())
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var cinemas = await _context.Cinemas
                .Where(c => c.City.ToLower() == city.ToLower())
                .ToListAsync();

            return cinemas.ToResponse();
        }

        public async Task<Result> AddCinema(AddCinemaRequest request)
        {
            if (!_tenantProvider.HasTenant())
            {
                return CinemaErrors.MissingTenantHeader();
            }

            // var cinema = CinemaFactory.CreateCinema(_tenantProvider.GetTenantId(), request);
            var cinema = request.ToEntity(_tenantProvider.GetTenantId());

            await _context.Cinemas.AddAsync(cinema);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return CinemaErrors.UnprocessableEntity(ex.Message);
                throw;
            }
        }

        public async Task<Result> UpdateCinema(UpdateCinemaRequest updatedCinema, int cinemaId)
        {
            if (!_tenantProvider.HasTenant())
            {
                return CinemaErrors.MissingTenantHeader();
            }

            var existing = await _context.Cinemas
                .FirstOrDefaultAsync(c => c.Id == cinemaId);

            if (existing is null)
            {
                return CinemaErrors.CinemaNotFound(cinemaId);
            }

            existing.Name = updatedCinema.Name;
            existing.Address = updatedCinema.Address;
            existing.City = updatedCinema.City;
            existing.State = updatedCinema.State;
            existing.ZipCode = updatedCinema.ZipCode;

            _context.Cinemas.Update(existing);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return CinemaErrors.UnprocessableEntity(ex.Message);
                throw;
            }
        }

        public async Task<Result> DeleteCinema(int id)
        {
            if (!_tenantProvider.HasTenant())
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

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return CinemaErrors.UnprocessableEntity(ex.Message);
                throw;
            }
        }
    }
}
