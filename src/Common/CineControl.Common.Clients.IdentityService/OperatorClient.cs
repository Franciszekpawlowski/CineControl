using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.Clients.IdentityService.Models.Account;
using CineControl.Common.Clients.IdentityService.Models.Operator;
using CineControl.Common.Clients.IdentityService.Options;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace CineControl.Common.Clients.IdentityService;

public class OperatorClient : IOperatorClient, IDisposable
{
    readonly string _baseUrl;
    readonly RestClient _client;
    readonly IServiceScopeFactory _serviceScopeFactory;
    readonly IdentityServiceClientOptions _identityOptions;
    public OperatorClient(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<IdentityServiceClientOptions> identityOptions
    )
    {
        _identityOptions = identityOptions.Value;
        _baseUrl = _identityOptions.BaseUrl;
        _serviceScopeFactory = serviceScopeFactory;
        var options = new RestClientOptions($"{_baseUrl}/api/v1");
        _client = new RestClient(options);
    }


    public async Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest("/Operator/Login");
        request.AddJsonBody(loginRequestModel);
        request.AddHeader(TenantFieldNames.HeaderName, _tenantProvider.GetTenantId().ToString());
        var response = await _client.ExecutePostAsync<LoginResponseModel>(request);
        return response.ToResult();
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Result> RegisterAsync(RegisterRequestModel registerRequestModel)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest("/Operator/Register");
        request.AddJsonBody(registerRequestModel);
        request.AddHeader(TenantFieldNames.HeaderName, _tenantProvider.GetTenantId().ToString());
        var response = await _client.ExecutePostAsync(request);
        return response.ToResult();
    }

    public async Task<Result> RegisterByAdminAsync(
        RegisterByAdminRequestModel registerRequestModel,
        string Token
    )
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest("/Operator/RegisterByAdmin")
        {
            Authenticator = new JwtAuthenticator(Token)
        };
        request.AddJsonBody(registerRequestModel);
        var response = await _client.ExecutePostAsync(request);
        return response.ToResult();
    }

    public async Task<ResultT<IEnumerable<GetUserResponseModel>>> GetTenantOperatorAsync(Guid id, string Token)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest($"/Operator/GetOperators/{id}")
        {
            Authenticator = new JwtAuthenticator(Token)
        };
        var response = await _client.ExecutePostAsync<IEnumerable<GetUserResponseModel>>(request);
        return response.ToResult();
    }

    public async Task<ResultT<GetUserResponseModel>> GetTenantOperatorByIdAsync(Guid tenantId, Guid id, string Token)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest($"/Operator/GetOperatorById/{tenantId}/{id}")
        {
            Authenticator = new JwtAuthenticator(Token)
        };
        var response = await _client.ExecutePostAsync<GetUserResponseModel>(request);
        return response.ToResult();
    }

    public async Task<Result> DeleteAsync(Guid tenantId, Guid id, string Token)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest($"/Operator/DeleteOperator/{tenantId}/{id}")
        {
            Authenticator = new JwtAuthenticator(Token)
        };
        var response = await _client.ExecuteDeleteAsync(request);
        return response.ToResult();
    }
}