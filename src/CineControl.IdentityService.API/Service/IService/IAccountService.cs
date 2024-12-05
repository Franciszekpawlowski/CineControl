using CineControl.Common.Results;
using CineControl.IdentityService.API.Models.DTOs.Auth;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IAccountService
    {
        Task<ResultT<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        Task<Result> RegisterAsync(RegisterRequest registerRequest);
        Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}