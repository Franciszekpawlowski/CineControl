using CineControl.Common.Clients.IdentityService.Models.Account;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.IdentityService.IClients;

public interface IAdminClient
{
    Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel);
}