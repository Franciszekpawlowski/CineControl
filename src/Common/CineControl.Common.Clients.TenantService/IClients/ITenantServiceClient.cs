using CineControl.Common.Clients.TenantService.Models.GetTenant;
using CineControl.Common.Results;
namespace CineControl.Common.Clients.TenantService.IClients;

public interface ITenantServiceClient
{
    Task<ResultT<GetTenantByIdResponseModel>> GetTenantByIdAsync(int tenantId);
    // Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token);
}
