using System.Security.Claims;
using CineControl.IdentityService.API.Models;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(ApplicationUser applicationUser);
        ClaimsPrincipal? GetTokenPrincipal(string token);
        string GenerateRefreshToken();
    }
}