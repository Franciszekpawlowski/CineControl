using CineControl.Common.Clients.SeanceService.IClients;
using CineControl.Common.JWTProvider;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.OperatorPanel.Errors;
using CineControl.OperatorPanel.Extensions;
using CineControl.OperatorPanel.Models.DTOs.Seances;
using CineControl.OperatorPanel.Service.IService;

namespace CineControl.OperatorPanel.Service;

public class SeanceService(ISeanceClient seanceClient,
    IHttpContextAccessor httpContextAccessor,
    ITenantProvider tenantProvider,
    IJWTProvider jwtProvider
) : ISeanceService
{
    private readonly ISeanceClient _seanceClient = seanceClient;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly IJWTProvider _jwtProvider = jwtProvider;

    public async Task<Result> AddSeanceAsync(AddSeanceRequest addSeanceRequest)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovie = await _seanceClient.AddSeanceAsync(addSeanceRequest.ToRequest(),TenantId);

        if (!GetMovie.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<Result> DeleteSeanceAsync(int id)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovie = await _seanceClient.DeleteSeanceAsync(id,TenantId);

        if (!GetMovie.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<ResultT<IEnumerable<GetSeanceResponse>>> GetSeancesAsync()
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetCinema = await _seanceClient.GetSeancesAsync(TenantId);
        
        if ( !GetCinema.IsSuccess )
        {
            return CinemaServiceErrors.Failure();
        }
        return GetCinema.Value.ToResponse();
    }

    public async Task<ResultT<GetSeanceResponse>> GetSeancesByIdAsync(int Id)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetCinema = await _seanceClient.GetSeancesByIdAsync(Id,TenantId);
        
        if ( !GetCinema.IsSuccess )
        {
            return CinemaServiceErrors.Failure();
        }
        return GetCinema.Value.ToResponse();
    }

    public async Task<Result> UpdateSeanceAsync(int id, UpdateSeanceRequest request)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovie = await _seanceClient.UpdateSeanceAsync(id,request.ToRequest(),TenantId);

        if (!GetMovie.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }
}
