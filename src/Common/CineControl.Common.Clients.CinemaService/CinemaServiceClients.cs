using CineControl.Common.Clients.CinemaService.Errors;
using CineControl.Common.Clients.CinemaService.IClients;
using CineControl.Common.Clients.CinemaService.Models.AddCinema;
using CineControl.Common.Clients.CinemaService.Models.GetCinemas;
using CineControl.Common.Clients.CinemaService.Models.UpdateCinema;
using CineControl.Common.Results;
using RestSharp;

namespace CineControl.Common.Clients.CinemaService;

public class CinemaServiceClients : ICinemaServiceClients, IDisposable
{
    readonly string _baseUrl = "http://localhost:5000/api/v1";
    readonly RestClient _client;

    public CinemaServiceClients()
    {
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }

    public async Task<ResultT<IEnumerable<GetCinemasResponseModel>>> GetCinemasAsync()
    {
        var request = new RestRequest("/Cinemas");
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

    public async Task<ResultT<GetCinemasResponseModel>> GetCinemaAsync(int id)
    {
        var request = new RestRequest($"/Cinemas/{id}");
        var responseModel = await _client.GetAsync<GetCinemasResponseModel>(request);
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<ResultT<GetCinemasResponseModel>> GetCinemaByCity(string city)
    {
        var request = new RestRequest($"/Cinemas/ByCity/{city}");
        var responseModel = await _client.GetAsync<GetCinemasResponseModel>(request);
        if(responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<Result> AddCinemaAsync(AddCinemaRequestModel model)
    {
        var request = new RestRequest("/Cinemas/AddCinema");
        request.AddJsonBody(model);
        var response = await _client.PostAsync(request);
        if (response == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }

    public async Task<Result> UpdateCinemaAsync(int id, UpdateCinemaRequestModel model)
    {
        var request = new RestRequest($"/Cinemas/{id}");
        request.AddJsonBody(model);
        var response = await _client.PutAsync(request);
        if (response == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }

    public async Task<Result> DeleteCinemaAsync(int id)
    {
        var request = new RestRequest($"/Cinemas/{id}");
        var response = await _client.DeleteAsync(request);
        if (response == null)
        {
            return ClientErrors.Failure;
        }
        return Result.Success();
    }
}
