using CineControl.Common.Clients.TenantService.Errors;
using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Clients.TenantService.Models.GetTenant;
using CineControl.Common.Results;
using RestSharp;

namespace CineControl.Common.Clients.TenantService;

public class TenantServiceClient : ITenantServiceClient, IDisposable
{
    readonly string _baseUrl = "http://localhost:5000/api/v1";
    readonly RestClient _client;
    public TenantServiceClient()
    {
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }


    public async Task<ResultT<GetTenantByIdResponseModel>> GetTenantByIdAsync(int tenantId)
    {
        var request = new RestRequest($"/Tenant/{tenantId}");   
        var responseModel = await _client.GetAsync<GetTenantByIdResponseModel>(request);

        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
