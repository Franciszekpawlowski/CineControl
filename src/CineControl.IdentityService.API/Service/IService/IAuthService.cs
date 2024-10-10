using CineControl.IdentityService.API.Models.Request.Auth;
using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.Auth;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IAuthService
    {
        Task<GenericResults<LoginResults>> LoginAsync(LoginRequest loginRequest);
        Task<GenericResults<RegistationResults>> RegisterAsync(RegisterRequest registerRequest);
        Task<GenericResults<RefreshTokenResult>> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}