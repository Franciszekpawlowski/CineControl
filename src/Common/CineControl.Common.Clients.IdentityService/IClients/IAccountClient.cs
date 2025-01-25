using CineControl.Common.Clients.IdentityService.Models.Account;
using CineControl.Common.Results;
namespace CineControl.Common.Clients.IdentityService.IClients;

public interface IAccountClient
{
    Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel);
    Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token);
}