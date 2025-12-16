using System.Security.Claims;
using CineControl.IdentityService.API.Models;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(ApplicationUser applicationUser);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        ClaimsPrincipal? GetPrincipalFromToken(string? token);
        string GenerateRefreshToken();
    }
}