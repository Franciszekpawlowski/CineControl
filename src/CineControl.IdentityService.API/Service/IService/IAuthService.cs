
using CineControl.IdentityService.API.Models.DTO;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<string> RegisterAsync(RegisterRequestRequestDTO registerRequest);
        // Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}