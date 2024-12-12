using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs;
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

    public async Task<ResultT<TheaterResponse>> GetTheaterById(int theaterId)
    {
        if (!_tenantProvider.HasTenant)
        {
            return CinemaErrors.MissingTenantHeader();
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
        if (!_tenantProvider.HasTenant)
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

}
