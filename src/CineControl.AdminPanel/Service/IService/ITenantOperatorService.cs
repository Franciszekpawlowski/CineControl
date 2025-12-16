using CineControl.AdminPanel.Models.DTO.TetantOperator;
using CineControl.Common.Results;

namespace CineControl.AdminPanel.Service.IService;

public interface ITenantOperatorService
{
    Task<Result> CreateTenantOperatorAsync(CreateOperatorRequest createOperatorRequest);
    Task<ResultT<GetTenantOperatorResponse>> GetTenantOperatorByIdAsync(Guid tenant, Guid id);
    Task<Result> DeleteTenantOperatorAsync(Guid tenantId,Guid id);
    Task<ResultT<IEnumerable<GetTenantOperatorResponse>>> GetTenantOperatorAsync(Guid id);
}
