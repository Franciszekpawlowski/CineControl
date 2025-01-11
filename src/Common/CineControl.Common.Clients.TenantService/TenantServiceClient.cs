using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Clients.TenantService.Models.Tenant;
using CineControl.Common.Clients.TenantService.Options;
using CineControl.Common.Results;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CineControl.Common.Clients.TenantService;

public class TenantServiceClient : ITenantServiceClient, IDisposable
{
    readonly string _baseUrl;
    readonly TenantServiceClientOptions _tenantOptions;
    readonly RestClient _client;
    public TenantServiceClient(IOptions<TenantServiceClientOptions> tenantOptions)
    {
        _tenantOptions = tenantOptions.Value;
        _baseUrl = _tenantOptions.BaseUrl;
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }


    public async Task<ResultT<GetResponseModel>> GetAsync(Guid tenantId)
    {
        var request = new RestRequest($"/api/v1/Tenant/{tenantId}");   
        var response = await _client.ExecuteGetAsync<GetResponseModel>(request);

        return response.ToResult();
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<ResultT<IEnumerable<GetResponseModel>>> GetAllAsync()
    {
        var request = new RestRequest("/Tenant");
        var response = await _client.ExecuteGetAsync<IEnumerable<GetResponseModel>>(request);
        return response.ToResult();
    }

    public async Task<ResultT<GetResponseModel>> CreateAsync(CreateTenantRequestModel requestModel)
    {
        var request = new RestRequest("/Tenant/Create");
        request.AddJsonBody(requestModel);
        var response = await _client.ExecutePostAsync<GetResponseModel>(request);
        return response.ToResult();
    }

    public async Task<Result> UpdateAsync(Guid tenantId ,UpdateTenantRequestModel requestModel)
    {
        var request = new RestRequest($"/Tenant/{tenantId}");
        request.AddJsonBody(requestModel);
        var response = await _client.ExecutePutAsync(request);
        return response.ToResult();
        
    }

    public async Task<Result> DeleteAsync(Guid tenantId)
    {
        var requestModel = new RestRequest($"/Tenant/{tenantId}");
        var response = await _client.ExecuteDeleteAsync(requestModel);
        return response.ToResult();
    }
}
