using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CineControl.CinemaService.API.Service;

public class TheaterServices(CinemaContext context, ITenantProvider tenantProvider) : ITheaterServices
{
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly CinemaContext _context = context;

    public async Task<ResultT<TheaterResponse>> GetTheaterById(int cinemaId,int theaterId)
    {
        if (!_tenantProvider.HasTenant())
        {
            return CinemaErrors.MissingTenantHeader();
        }

        var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == cinemaId);

        if (cinema is null)
        {
            return CinemaErrors.CinemaNotFound(cinemaId);
        }

        var theater = await _context.Theaters
            .Include(t => t.Seats)
            .FirstOrDefaultAsync(t => t.Id == theaterId);

        return theater is not null
            ? theater.ToResponse()
            : CinemaErrors.TheaterNotFound(theaterId);
    }

    public async Task<ResultT<IEnumerable<TheaterResponse>>> GetTheatersByCinemaId(int cinemaId)
    {
        if (!_tenantProvider.HasTenant())
        {
            return CinemaErrors.MissingTenantHeader();
        }

        var cinema = await _context.Cinemas
            .Include(c => c.Theaters)
            .ThenInclude(t => t.Seats)
            .FirstOrDefaultAsync(c => c.Id == cinemaId);

        return cinema is not null
            ? cinema.Theaters.ToResponse()
            : CinemaErrors.CinemaNotFound(cinemaId);
    }

    public async Task<Result> AddTheater(int cinemaId, AddTheaterRequest request)
    {
        if (!_tenantProvider.HasTenant())
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
            TenantId = _tenantProvider.GetTenantId(),
            CinemaId = cinemaId,
            Name = request.Name,
            // Seats = CinemaFactory.GenerateSeats(_tenantProvider.GetTenantId(),theaterid, request.SeatingCapacity, request.SeatsPerRow)
        };

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.SaveChangesAsync();
            var theaterId = theater.Id;

            theater.Seats = CinemaFactory.GenerateSeats(_tenantProvider.GetTenantId(), theaterId, request.SeatingCapacity, request.SeatsPerRow);

            _context.Theaters.Update(theater);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return CinemaErrors.UnprocessableEntity(ex.Message);
            throw;
        }
    }

    public async Task<Result> RemoveTheater(int cinemaId, int theaterId)
    {
        if (!_tenantProvider.HasTenant())
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

    public async Task<Result> UpdateTheater(int cinemaId, int theaterId, UpdateTheaterRequest request)
    {
        if (!_tenantProvider.HasTenant())
        {
            return CinemaErrors.MissingTenantHeader();
        }

        var cinema = await _context.Cinemas
            .FirstOrDefaultAsync(c => c.Id == cinemaId);

        if (cinema is null)
        {
            return CinemaErrors.CinemaNotFound(cinemaId);
        }

        var theater = await _context.Theaters
            .Include(t => t.Seats)
            .FirstOrDefaultAsync(t => t.Id == theaterId);

        if (theater == null)
        {
            return CinemaErrors.TheaterNotFound(theaterId);
        }

        theater.Name = request.Name;

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            theater.Seats.Clear();
            theater.Seats = CinemaFactory.GenerateSeats(_tenantProvider.GetTenantId(), theaterId, request.SeatingCapacity, request.SeatsPerRow);

            // _context.Seats.RemoveRange(theater.Seats);
            // _context.SaveChanges();

            // theater.Seats = CinemaFactory.GenerateSeats(_tenantProvider.GetTenantId(), theaterId, request.SeatingCapacity, request.SeatsPerRow);

            _context.Theaters.Update(theater);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
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
