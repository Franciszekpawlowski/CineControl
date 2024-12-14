using CineControl.Common.Results;
using CineControl.OperatorPanel.Models.GetUser;
using CineControl.OperatorPanel.Models.UserLogin;

namespace CineControl.OperatorPanel.Service.IService;

public interface IAuthService
{
    Task<Result> LoginAsync(UserLoginRequest userRequest);
    Task<ResultT<GetUserResult>> GetUserAsync();
    Task<Result> LogoutAsync();
}
