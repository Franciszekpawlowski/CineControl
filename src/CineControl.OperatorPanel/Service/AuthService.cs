using CineControl.OperatorPanel.Models;
using CineControl.OperatorPanel.Models.Result;
using CineControl.OperatorPanel.Service.IService;

namespace CineControl.OperatorPanel.Service;

public class AuthService : IAuthService
{
    public Task<LoginResult> LoginAsync(UserForLoginDTO UserForLoginDTO)
    {
        
    }
}