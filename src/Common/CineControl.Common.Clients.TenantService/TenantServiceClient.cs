using CineControl.Common.Clients.TenantService.Errors;
using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Clients.TenantService.Models.Tenant;
using CineControl.Common.Results;
using RestSharp;

namespace CineControl.Common.Clients.TenantService;

public class TenantServiceClient : ITenantServiceClient, IDisposable
{
    readonly string _baseUrl = "http://localhost:5188/api/v1";
    readonly RestClient _client;
    public TenantServiceClient()
    {
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }


    public async Task<ResultT<GetResponseModel>> GetAsync(Guid tenantId)
    {
        var request = new RestRequest($"/Tenant/{tenantId}");   
        var responseModel = await _client.GetAsync<GetResponseModel>(request);

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

    public async Task<ResultT<IEnumerable<GetResponseModel>>> GetAllAsync()
    {
        var request = new RestRequest("/Tenant");
        var responseModel = await _client.GetAsync<IEnumerable<GetResponseModel>>(request);
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel.ToList();
    }

    public async Task<ResultT<GetResponseModel>> CreateAsync(CreateTenantRequestModel requestModel)
    {
        var request = new RestRequest("/Tenant/Create");
        request.AddJsonBody(requestModel);
        var responseModel = await _client.PostAsync<GetResponseModel>(request);
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<Result> UpdateAsync(Guid tenantId ,UpdateTenantRequestModel requestModel)
    {
        var request = new RestRequest($"/Tenant/{tenantId}");
        request.AddJsonBody(requestModel);
        var responseModel = await _client.PutAsync(request);
        if(responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
        
    }

    public async Task<Result> DeleteAsync(Guid tenantId)
    {
        var requestModel = new RestRequest($"/Tenant/{tenantId}");
        var responseModel = await _client.DeleteAsync(requestModel);
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }
}
