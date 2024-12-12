using CineControl.Common.Clients.IdentityService.Errors;
using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.Clients.IdentityService.Models.GetUser;
using CineControl.Common.Clients.IdentityService.Models.Login;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Authenticators;

namespace CineControl.Common.Clients.IdentityService;

public partial class IdentityServiceClient : IIdentityServiceClient, IDisposable
{
    readonly string _baseUrl = "http://localhost:5093/api/v1";
    readonly RestClient _client;
    readonly IServiceScopeFactory _serviceScopeFactory;
    public IdentityServiceClient(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }


    public async Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();

        var request = new RestRequest("/Account/Login");
        request.AddJsonBody(loginRequestModel);
        request.AddHeader(TenantFieldNames.HeaderName, _tenantProvider.TenantId.ToString());
        LoginResponseModel responseModel;
        try
        {
            responseModel = await _client.PostAsync<LoginResponseModel>(request);
        }
        catch
        {
            return ClientErrors.Failure;
        }

        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();

        var request = new RestRequest("/Account/GetUser")
        {
            Authenticator = new JwtAuthenticator(Token)
        };
        request.AddHeader(TenantFieldNames.HeaderName, _tenantProvider.TenantId.ToString());

        var responseModel = await _client.GetAsync<GetUserResponseModel>(request);
        if (responseModel == null)
        {
            return ClientErrors.AccessUnauthorized;
        }
        return responseModel;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
