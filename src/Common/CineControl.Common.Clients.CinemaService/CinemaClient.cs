using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.Clients.CinemaService.Models.CinemaClient;
using CineControl.Common.Clients.CinemaService.Options;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CineControl.Common.Clients.CinemaService;

public class CinemaClient : ICinemaClient, IDisposable
{
    readonly string _baseUrl;
    readonly RestClient _client;
    readonly IServiceProvider  _serviceScopeFactory;

    readonly CinemaServiceClientOptions _cinemaOptions;

    public CinemaClient(
        IServiceProvider serviceScopeFactory,
        IOptions<CinemaServiceClientOptions> cinemaOptions
    )
    {
        _cinemaOptions = cinemaOptions.Value;
        _baseUrl = _cinemaOptions.BaseUrl;
        _serviceScopeFactory = serviceScopeFactory;
        var options = new RestClientOptions($"{_baseUrl}/api/v1");
        _client = new RestClient(options);
    }

    public async Task<ResultT<IEnumerable<GetCinemasResponseModel>>> GetCinemasAsync(string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest("/Cinemas");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.ExecuteGetAsync<IEnumerable<GetCinemasResponseModel>>(request);

        return responseModel.ToResult();
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<ResultT<GetCinemasResponseModel>> GetCinemaAsync(int id, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Cinemas/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var responseModel = await _client.ExecuteGetAsync<GetCinemasResponseModel>(request);

        return responseModel.ToResult();
    }

    public async Task<ResultT<GetCinemasResponseModel>> GetCinemaByCity(string city,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Cinemas/ByCity/{city}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var responseModel = await _client.ExecuteGetAsync<GetCinemasResponseModel>(request);
        return responseModel.ToResult();
    }

    public async Task<Result> AddCinemaAsync(AddCinemaRequestModel model,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }
        var request = new RestRequest("/Cinemas");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddJsonBody(model);
        var response = await _client.ExecutePostAsync(request);

        return response.ToResult();
    }

    public async Task<Result> UpdateCinemaAsync(int id, UpdateCinemaRequestModel model,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Cinemas/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddJsonBody(model);
        var response = await _client.ExecutePutAsync(request);
        return response.ToResult();
    }

    public async Task<Result> DeleteCinemaAsync(int id,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }
        var request = new RestRequest($"/Cinemas/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var response = await _client.ExecuteDeleteAsync(request);
        return response.ToResult();
    }
}
