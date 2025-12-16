using CineControl.CinemaService.API.Data;
using CineControl.CinemaService.API.Errors;
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

    public async Task<ResultT<IEnumerable<SeatResponse>>> GetSeatsByTheaterId(int cinemaId,int theaterId)
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
            ? theater.Seats.ToResponse()
            : CinemaErrors.TheaterNotFound(theaterId);
    }
}
