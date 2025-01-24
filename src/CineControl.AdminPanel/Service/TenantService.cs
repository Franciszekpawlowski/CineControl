using CineControl.AdminPanel.Errors;
using CineControl.AdminPanel.Models.DTO.Tenant;
using CineControl.AdminPanel.Service.IService;
using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Results;

namespace CineControl.AdminPanel.Service;

public class TenantService(ITenantServiceClient tenantServiceClient) : ITenantService
{

    private readonly ITenantServiceClient _tenantServiceClient = tenantServiceClient;

    public async Task<Result> CreateTenantAsync(CreateTenantRequest request)
    {
        var result = await _tenantServiceClient.CreateAsync(request.ToRequest());
        if (!result.IsSuccess)
        {
            return TenantServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var result = await _tenantServiceClient.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return TenantServiceErrors.Failure();
        }
        return Result.Success();    
    }

    public async Task<ResultT<GetTenantResponse>> GetByIdAsync(Guid id)
    {
        var result = await _tenantServiceClient.GetAsync(id);
        if (!result.IsSuccess)
        {
            return TenantServiceErrors.Failure();
        }
        return result.Value.ToResponse();
    }

    public async Task<Result> UpdateAsync(Guid id ,UpdateTenantRequest request)
    {
        var result = await _tenantServiceClient.UpdateAsync(id, request.ToRequest());
        if (!result.IsSuccess)
        {
            return TenantServiceErrors.Failure();
        }
        return Result.Success();    
    }

    async Task<ResultT<IEnumerable<GetTenantResponse>>> ITenantService.GetTenantsAsync()
    {
        var getTenant = await _tenantServiceClient.GetAllAsync();
        if(!getTenant.IsSuccess)
        {
            return TenantServiceErrors.Failure();
        }

        return getTenant.Value.ToResponse();
    }
}
