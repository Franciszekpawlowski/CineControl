using CineControl.Common.Clients.AuthService.Models.GetUser;
using CineControl.Common.Clients.AuthService.Models.Login;
using CineControl.Common.Results;
namespace CineControl.Common.Clients.AuthService.IClients;

public interface IIdentityServiceClient
{
    Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel);
    Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token);
}