using CineControl.Common.Clients.IdentityService.Models.Account;
using CineControl.Common.Clients.IdentityService.Models.Operator;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.IdentityService.IClients;

public interface IOperatorClient
{
    Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel);
    Task<Result> RegisterAsync(RegisterRequestModel registerRequestModel);
    Task<Result> RegisterByAdminAsync(RegisterByAdminRequestModel registerRequestModel,string Token);
    Task<ResultT<IEnumerable<GetUserResponseModel>>> GetTenantOperatorAsync(Guid id,string token);
    Task<ResultT<GetUserResponseModel>> GetTenantOperatorByIdAsync(Guid tenantId,Guid id,string token);
    Task<Result> DeleteAsync(Guid tenantId,Guid id,string token);
}
