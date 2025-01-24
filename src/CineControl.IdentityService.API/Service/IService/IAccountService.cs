using CineControl.Common.Enums;
using CineControl.Common.Results;
using CineControl.IdentityService.API.Models.DTOs.Auth;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IAccountService
    {
        Task<ResultT<LoginResponse>> LoginAsync(LoginRequest loginRequest, Roles LoginRole = Roles.User);
        Task<Result> RegisterAsync(RegistrationRequest registerRequest,Roles RegisterRole = Roles.User);
        Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<Result> RegisterByAdminAsync(RegistrationRequestByAdmin registerRequest);
    }
}