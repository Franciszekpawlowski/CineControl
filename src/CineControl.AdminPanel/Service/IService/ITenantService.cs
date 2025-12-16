using CineControl.AdminPanel.Models.DTO.Tenant;
using CineControl.Common.Results;

namespace CineControl.AdminPanel.Service.IService;

public interface ITenantService
{
    Task<Result> CreateTenantAsync(CreateTenantRequest request);
    Task<Result> DeleteAsync(Guid id);
    Task<ResultT<GetTenantResponse>> GetByIdAsync(Guid id);
    Task<ResultT<IEnumerable<GetTenantResponse>>> GetTenantsAsync();
    Task<Result> UpdateAsync(Guid id,UpdateTenantRequest request);
}
