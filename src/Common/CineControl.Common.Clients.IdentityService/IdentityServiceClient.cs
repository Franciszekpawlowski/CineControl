using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.Clients.IdentityService.Models.GetUser;
using CineControl.Common.Clients.IdentityService.Models.Login;
using CineControl.Common.Clients.IdentityService.Options;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace CineControl.Common.Clients.IdentityService;

public partial class IdentityServiceClient : IIdentityServiceClient, IDisposable
{
    readonly string _baseUrl;
    readonly RestClient _client;
    readonly IServiceScopeFactory _serviceScopeFactory;
    readonly IdentityServiceClientOptions _identityOptions;
    public IdentityServiceClient(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<IdentityServiceClientOptions> identityOptions
    )
    {
        _identityOptions = identityOptions.Value;
        _baseUrl = _identityOptions.BaseUrl;
        _serviceScopeFactory = serviceScopeFactory;
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }


    public async Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
        var request = new RestRequest("/api/v1/Account/Login");
        request.AddJsonBody(loginRequestModel);
        request.AddHeader(TenantFieldNames.HeaderName, _tenantProvider.GetTenantId().ToString());
        var response = await _client.ExecutePostAsync<LoginResponseModel>(request);
        return response.ToResult();
    }

    public async Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();

        var request = new RestRequest("/api/v1/Account/GetUser")
        {
            Authenticator = new JwtAuthenticator(Token)
        };
        request.AddHeader(TenantFieldNames.HeaderName, _tenantProvider.GetTenantId().ToString());

        var response = await _client.ExecuteGetAsync<GetUserResponseModel>(request);
        return response.ToResult();

    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
