using System.Security.Claims;
using CineControl.IdentityService.API.Models;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
        ClaimsPrincipal? GetTokenPrincipal(string token);
        string GenerateRefreshToken();
    }
}