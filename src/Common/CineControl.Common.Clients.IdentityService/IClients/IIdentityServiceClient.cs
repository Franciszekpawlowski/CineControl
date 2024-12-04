using CineControl.Common.Clients.IdentityService.Models.GetUser;
using CineControl.Common.Clients.IdentityService.Models.Login;
using CineControl.Common.Results;
namespace CineControl.Common.Clients.IdentityService.IClients;

public interface IIdentityServiceClient
{
    Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel);
    Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token);
}