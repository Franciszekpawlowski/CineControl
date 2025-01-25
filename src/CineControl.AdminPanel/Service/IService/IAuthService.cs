using CineControl.AdminPanel.Models.GetUser;
using CineControl.AdminPanel.Models.UserLogin;
using CineControl.Common.Results;

namespace CineControl.AdminPanel.Service.IService;

public interface IAuthService
{
    Task<Result> LoginAsync(UserLoginRequest userRequest);
    Task<Result> LogoutAsync();
}
