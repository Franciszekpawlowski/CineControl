using System.IdentityModel.Tokens.Jwt;
using CineControl.Common;
using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.JWTProvider;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.OperatorPanel.Errors;
using CineControl.OperatorPanel.Extensions;
using CineControl.OperatorPanel.Models.DTOs.Cinemas;
using CineControl.OperatorPanel.Service.IService;

namespace CineControl.OperatorPanel.Service;

public class CinemaService(ICinemaServiceClient cinemaServiceClients,
    IHttpContextAccessor httpContextAccessor,
    ITenantProvider tenantProvider,
    IJWTProvider jwtProvider
) : ICinemaService
{
    private readonly ICinemaServiceClient _cinemaServiceClients = cinemaServiceClients;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly IJWTProvider _jwtProvider = jwtProvider;

    public async Task<ResultT<IEnumerable<GetCinemaResponse>>> GetCinemasAsync()
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetCinema = await _cinemaServiceClients.GetCinemasAsync(TenantId);
        
        if ( !GetCinema.IsSuccess )
        {
            return CinemaServiceErrors.Failure();
        }
        return GetCinema.Value.ToResponse();
    }
}
