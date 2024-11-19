using CineControl.OperatorPanel.Models;
using CineControl.OperatorPanel.Models.Result;

namespace CineControl.OperatorPanel.Service.IService;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(UserForLoginDTO UserForLoginDTO);
}
