using CineControl.AdminPanel.Errors;
using CineControl.AdminPanel.Models.DTO.TetantOperator;
using CineControl.AdminPanel.Service.IService;
using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.Results;

namespace CineControl.AdminPanel.Service;

public class TenantOperatorService(
    IOperatorClient operatorClient,
    IHttpContextAccessor httpContextAccessor

) : ITenantOperatorService
{
    private readonly IOperatorClient _operatorClient = operatorClient;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public async Task<Result> CreateTenantOperatorAsync(CreateOperatorRequest createOperatorRequest)
    {
        string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"].ToString();
        
        if (string.IsNullOrEmpty(token))
        {
            return AuthServiceErrors.AccessUnauthorized();
        }
        var result = await _operatorClient.RegisterByAdminAsync(createOperatorRequest.ToRequest(), token);
        if (!result.IsSuccess)
        {
            return AuthServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<Result> DeleteTenantOperatorAsync(Guid tenantId, Guid id)
    {
        string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"].ToString();
        
        if (string.IsNullOrEmpty(token))
        {
            return AuthServiceErrors.AccessUnauthorized();
        }
        var result = await _operatorClient.DeleteAsync(tenantId,id,token);
        if (!result.IsSuccess)
        {
            return AuthServiceErrors.Failure();
        }
        return Result.Success();

    }

    public async Task<ResultT<IEnumerable<GetTenantOperatorResponse>>> GetTenantOperatorAsync(Guid id)
    {
        string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"].ToString();
        
        if (string.IsNullOrEmpty(token))
        {
            return AuthServiceErrors.AccessUnauthorized();
        }
        var result = await _operatorClient.GetTenantOperatorAsync(id,token);
        if (!result.IsSuccess)
        {
            return AuthServiceErrors.Failure();
        }
        return result.Value.ToResponse();

    }

    public async Task<ResultT<GetTenantOperatorResponse>> GetTenantOperatorByIdAsync(Guid tenant, Guid id)
    {
        string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"].ToString();
        
        if (string.IsNullOrEmpty(token))
        {
            return AuthServiceErrors.AccessUnauthorized();
        }
        var result = await _operatorClient.GetTenantOperatorByIdAsync(tenant,id,token);
        if (!result.IsSuccess)
        {
            return AuthServiceErrors.Failure();
        }
        return result.Value.ToResponse();
    }
}
