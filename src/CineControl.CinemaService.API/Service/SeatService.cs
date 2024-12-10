using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Models.DTOs;
using CineControl.CinemaService.API.Models.DTOs.Seats;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CineControl.CinemaService.API.Service;

public class SeatService(CinemaContext dbContext, ITenantProvider tenantProvider) : ISeatService
{
    private readonly CinemaContext _context = dbContext;
    private readonly ITenantProvider _tenantProvider = tenantProvider;

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

    public async Task<ResultT<IEnumerable<SeatResponse>>> GetSeatsByTheaterId(int theaterId)
    {
        if (!_tenantProvider.HasTenant)
        {
            return CinemaErrors.MissingTenantHeader();
        }

        var theater = await _context.Theaters
            .Include(t => t.Seats)
            .FirstOrDefaultAsync(t => t.Id == theaterId);

        return theater is not null
            ? theater.Seats.ToResponse()
            : CinemaErrors.TheaterNotFound(theaterId);
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
