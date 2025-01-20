using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.JWTProvider;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.OperatorPanel.Errors;
using CineControl.OperatorPanel.Extensions;
using CineControl.OperatorPanel.Models.DTOs.Theaters;
using CineControl.OperatorPanel.Service.IService;

namespace CineControl.OperatorPanel.Service;

public class TheaterService(ITheaterClient theaterServiceClients,
    IHttpContextAccessor httpContextAccessor,
    ITenantProvider tenantProvider,
    IJWTProvider jwtProvider
) : ITheaterService
{
    private readonly ITheaterClient _theaterServiceClients = theaterServiceClients;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly IJWTProvider _jwtProvider = jwtProvider;

    public async Task<Result> AddTheaterAsync(int cinemaId, AddTheaterRequest request)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var response = await _theaterServiceClients.AddTheaterAsync(cinemaId, request.ToRequest(),TenantId);

        if (!response.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<Result> DeleteTheaterAsync(int cinemaId, int id)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var response = await _theaterServiceClients.DeleteTheaterAsync(cinemaId,id,TenantId);

        if (!response.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<ResultT<GetTheaterResponse>> GetTheaterByIdAsync(int cinemaId, int id)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetTheater = await _theaterServiceClients.GetTheaterByIdAsync(cinemaId,id,TenantId);

        if (!GetTheater.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return GetTheater.Value.ToResponse();
    }

    public async Task<ResultT<IEnumerable<GetTheaterResponse>>> GetTheatersAsync(int cinemaId)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetTheaters = await _theaterServiceClients.GetTheatersAsync(cinemaId,TenantId);

        if (!GetTheaters.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return GetTheaters.Value.ToResponse();
    }

    public async Task<Result> UpdateTheaterAsync(int cinemaId, int id, UpdateTheaterRequest request)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetTheater = await _theaterServiceClients.UpdateTheaterAsync(cinemaId,id,request.ToRequest(),TenantId);

        if (!GetTheater.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }
}
