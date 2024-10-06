using CineControl.IdentityService.API.Models.DTO;
using CineControl.IdentityService.API.Models.DTO.Request;
using CineControl.IdentityService.API.Models.DTO.Response;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<string> RegisterAsync(RegisterRequestRequestDTO registerRequest);
        Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequest);
    }
}