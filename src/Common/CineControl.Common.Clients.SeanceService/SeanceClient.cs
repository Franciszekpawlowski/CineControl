using CineControl.Common.Clients.SeanceService.IClients;
using CineControl.Common.Clients.SeanceService.Models.Seances;
using CineControl.Common.Clients.SeanceService.Options;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CineControl.Common.Clients.SeanceService;

public class SeanceClient : ISeanceClient, IDisposable
{
    readonly string _baseUrl;
    readonly RestClient _client;
    readonly IServiceScopeFactory _serviceScopeFactory;
    readonly SeanceServiceClientOptions _identityOptions;
    public SeanceClient(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<SeanceServiceClientOptions> identityOptions
    )
    {
        _identityOptions = identityOptions.Value;
        _baseUrl = _identityOptions.BaseUrl;
        _serviceScopeFactory = serviceScopeFactory;
        var options = new RestClientOptions($"{_baseUrl}/api/v1");
        _client = new RestClient(options);
    }

    public async Task<Result> AddSeanceAsync(AddSeanceRequestModel addSeanceRequestModel, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest("/Seances");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddBody(addSeanceRequestModel);

        var response = await _client.ExecutePostAsync(request);

        return response.ToResult();
    }

    public async Task<Result> DeleteSeanceAsync(int id, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Seances/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var response = await _client.ExecuteDeleteAsync(request);

        return response.ToResult();    
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<ResultT<IEnumerable<GetSeancesResponseModel>>> GetSeancesAsync(string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest("/Seances");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.ExecuteGetAsync<IEnumerable<GetSeancesResponseModel>>(request);

        return responseModel.ToResult();
    }

    public async Task<ResultT<IEnumerable<GetSeancesResponseModel>>> GetSeancesByCinemaIdAsync(int cinemaId, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Seances/bycinema/{cinemaId}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.ExecuteGetAsync<IEnumerable<GetSeancesResponseModel>>(request);

        return responseModel.ToResult();
    }

    public async Task<ResultT<GetSeancesResponseModel>> GetSeancesByIdAsync(int id, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Seances/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.ExecuteGetAsync<GetSeancesResponseModel>(request);

        return responseModel.ToResult();
    }

    public async Task<Result> UpdateSeanceAsync(int id, UpdateSeanceRequestModel updateSeanceRequestModel, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Seances/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddBody(updateSeanceRequestModel);

        var response = await _client.ExecutePutAsync(request);

        return response.ToResult();
    }
}
