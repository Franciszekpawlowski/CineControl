using CineControl.Common.Clients.CinemaService.Errors;
using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.Clients.CinemaService.Models.AddCinema;
using CineControl.Common.Clients.CinemaService.Models.GetCinemas;
using CineControl.Common.Clients.CinemaService.Models.UpdateCinema;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace CineControl.Common.Clients.CinemaService;

public class CinemaServiceClient : ICinemaServiceClient, IDisposable
{
    readonly string _baseUrl = "http://localhost:5112/api/v1";
    readonly RestClient _client;
    readonly IServiceProvider  _serviceScopeFactory;

    public CinemaServiceClient(IServiceProvider  serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }

    public async Task<ResultT<IEnumerable<GetCinemasResponseModel>>> GetCinemasAsync(string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.TenantId.ToString();
        }

        var request = new RestRequest("/Cinemas");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);

        var responseModel = await _client.GetAsync<IEnumerable<GetCinemasResponseModel>>(request);
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel.ToList();
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
            TenantId = _tenantProvider.TenantId.ToString();
        }

        var request = new RestRequest($"/Cinemas/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var responseModel = await _client.GetAsync<GetCinemasResponseModel>(request);
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<ResultT<GetCinemasResponseModel>> GetCinemaByCity(string city,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.TenantId.ToString();
        }

        var request = new RestRequest($"/Cinemas/ByCity/{city}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var responseModel = await _client.GetAsync<GetCinemasResponseModel>(request);
        if(responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<Result> AddCinemaAsync(AddCinemaRequestModel model,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.TenantId.ToString();
        }
        var request = new RestRequest("/Cinemas/AddCinema");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddJsonBody(model);
        var response = await _client.PostAsync(request);
        if (response == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }

    public async Task<Result> UpdateCinemaAsync(int id, UpdateCinemaRequestModel model,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.TenantId.ToString();
        }

        var request = new RestRequest($"/Cinemas/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        request.AddJsonBody(model);
        var response = await _client.PutAsync(request);
        if (response == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }

    public async Task<Result> DeleteCinemaAsync(int id,string TenantId = null)
    {
        if (TenantId == null)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
            TenantId = _tenantProvider.TenantId.ToString();
        }
        var request = new RestRequest($"/Cinemas/{id}");
        request.AddHeader(TenantFieldNames.HeaderName, TenantId);
        var response = await _client.DeleteAsync(request);
        if (response == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }
}
