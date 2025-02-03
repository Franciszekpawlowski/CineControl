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
        var options = new RestClientOptions($"{_baseUrl}/api/v1");
        _client = new RestClient(options);
    }


    public async Task<ResultT<GetTenantResponseModel>> GetAsync(Guid tenantId)
    {
        var request = new RestRequest($"/Tenant/{tenantId}");
        var response = await _client.ExecuteGetAsync<GetTenantResponseModel>(request);

        return response.ToResult();
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<ResultT<IEnumerable<GetTenantResponseModel>>> GetAllAsync()
    {
        var request = new RestRequest("/Tenant");
        var response = await _client.ExecuteGetAsync<IEnumerable<GetTenantResponseModel>>(request);
        return response.ToResult();
    }

    public async Task<Result> CreateAsync(CreateTenantRequestModel requestModel)
    {
        var request = new RestRequest("/Tenant");
        request.AddJsonBody(requestModel);
        var response = await _client.ExecutePostAsync(request);
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
