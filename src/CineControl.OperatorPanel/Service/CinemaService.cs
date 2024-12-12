using System.IdentityModel.Tokens.Jwt;
using CineControl.Common;
using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.OperatorPanel.Errors;
using CineControl.OperatorPanel.Models.DTOs.Cinemas;
using CineControl.OperatorPanel.Service.IService;

namespace CineControl.OperatorPanel.Service;

public class CinemaService(ICinemaServiceClient cinemaServiceClients,
    IHttpContextAccessor httpContextAccessor,
    ITenantProvider tenantProvider
) : ICinemaService
{
    private readonly ICinemaServiceClient _cinemaServiceClients = cinemaServiceClients;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ITenantProvider _tenantProvider = tenantProvider;

    public async Task<ResultT<IEnumerable<GetCinemaResponse>>> GetCinemasAsync()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
        var tokenhandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenhandler.ReadJwtToken(token);
        var TenantId = jwtToken.Claims.FirstOrDefault(c => c.Type == CustomClaims.TenantId)?.Value;
        _tenantProvider.SetTenant(Guid.Parse(TenantId));
        var GetCinemaResponse = await _cinemaServiceClients.GetCinemasAsync(TenantId);
        if ( GetCinemaResponse == null )
        {
            return CinemaServiceErrors.Failure();
        }
        return GetCinemaResponse.Value.Select(x => new GetCinemaResponse(x)).ToList();
    }
}
