using CineControl.Common.Clients.TenantService.Models.Tenant;
using CineControl.Common.Results;
namespace CineControl.Common.Clients.TenantService.IClients;

public interface ITenantServiceClient
{
    Task<ResultT<IEnumerable<GetResponseModel>>> GetAllAsync();
    Task<ResultT<GetResponseModel>> GetAsync(Guid tenantId);
    Task<ResultT<GetResponseModel>> CreateAsync(CreateTenantRequestModel requestModel);
    Task<Result> UpdateAsync(Guid tenantId, UpdateTenantRequestModel requestModel);
    Task<Result> DeleteAsync(Guid tenantId);
}
