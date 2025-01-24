using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.Clients.CinemaService.Models.TheaterClient;
using CineControl.Common.Clients.CinemaService.Options;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CineControl.Common.Clients.CinemaService;

public class TheaterClient : ITheaterClient, IDisposable
{
    readonly string _baseUrl;
    readonly RestClient _client;
    readonly IServiceProvider  _serviceScopeFactory;

    readonly CinemaServiceClientOptions _cinemaOptions;

    public TheaterClient(
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

    public async Task<ResultT<IEnumerable<GetTheaterResponseModel>>> GetTheatersAsync(int cinemaId,string? TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Cinemas/{cinemaId}/theaters");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var responseModel = await _client.ExecuteGetAsync<IEnumerable<GetTheaterResponseModel>>(request);
        return responseModel.ToResult();
    }

    public async Task<ResultT<GetTheaterResponseModel>> GetTheaterByIdAsync(int cinemaId,int theaterId,string? TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Cinemas/{cinemaId}/theaters/{theaterId}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var responseModel = await _client.ExecuteGetAsync<GetTheaterResponseModel>(request);
        return responseModel.ToResult();
    }

    public async Task<Result> AddTheaterAsync(int cinemaId, AddTheaterRequestModel model,string? TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }
        var request = new RestRequest($"/Cinemas/{cinemaId}/theaters");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddJsonBody(model);
        var response = await _client.ExecutePostAsync(request);
        return response.ToResult();
    }

    public async Task<Result> UpdateTheaterAsync(int cinemaId, int theaterId, UpdateTheaterRequestModel model,string? TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }
        var request = new RestRequest($"/Cinemas/{cinemaId}/theaters/{theaterId}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddJsonBody(model);
        var response = await _client.ExecutePutAsync(request);
        return response.ToResult();
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteTheaterAsync(int cinemaId, int theaterId, string? TenantId)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }
        var request = new RestRequest($"/Cinemas/{cinemaId}/theaters/{theaterId}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var response = await _client.ExecuteDeleteAsync(request);
        return response.ToResult();
    }
}
