using CineControl.Common.Clients.SeanceService.IClients;
using CineControl.Common.Clients.SeanceService.Models.Movies;
using CineControl.Common.Clients.SeanceService.Options;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CineControl.Common.Clients.SeanceService;

public class MoviesClient : IMoviesClient, IDisposable
{
    readonly string _baseUrl;
    readonly RestClient _client;
    readonly IServiceScopeFactory _serviceScopeFactory;
    readonly SeanceServiceClientOptions _identityOptions;
    public MoviesClient(
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

    public async Task<Result> AddMovieAsync(AddMovieRequestModel addMovieRequestModel, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest("/Movies");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddBody(addMovieRequestModel);

        var response = await _client.ExecutePostAsync(request);

        return response.ToResult();
    }

    public async Task<Result> DeleteMovieAsync(int id, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Movies/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var response = await _client.ExecuteDeleteAsync(request);

        return response.ToResult();
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<ResultT<IEnumerable<GetMoviesResponseModel>>> GetMoviesAsync(string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest("/Movies");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.ExecuteGetAsync<IEnumerable<GetMoviesResponseModel>>(request);

        return responseModel.ToResult();
    }

    public async Task<ResultT<GetMoviesResponseModel>> GetMoviesByIdAsync(int id, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Movies/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.ExecuteGetAsync<GetMoviesResponseModel>(request);

        return responseModel.ToResult();
    }

    public async Task<Result> UpdateMovieAsync(int id, UpdateMovieRequestModel updateMovieRequestModel, string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.GetTenantId().ToString();
        }

        var request = new RestRequest($"/Movies/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddBody(updateMovieRequestModel);

        var response = await _client.ExecutePutAsync(request);

        return response.ToResult();

    }
}
