using CineControl.Common.Clients.AuthService.Errors;
using CineControl.Common.Clients.AuthService.IClients;
using CineControl.Common.Clients.AuthService.Models.GetUser;
using CineControl.Common.Clients.AuthService.Models.Login;
using CineControl.Common.Results;
using RestSharp;
using RestSharp.Authenticators;

namespace CineControl.Common.Clients.AuthService;

public partial class IdentityServiceClient : IIdentityServiceClient, IDisposable
{
    readonly string _baseUrl = "http://localhost:5093/api/v1";
    readonly RestClient _client;
    public IdentityServiceClient()
    {
        var options = new RestClientOptions(_baseUrl);
        _client = new RestClient(options);
    }


    public async Task<ResultT<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
    {
        var request = new RestRequest("/Auth/Login");
        request.AddJsonBody(loginRequestModel);
        var responseModel = await _client.PostAsync<LoginResponseModel>(request);
        
        if (responseModel == null)
        {
            return ClientErrors.Failure;
        }
        return responseModel;
    }

    public async Task<ResultT<GetUserResponseModel>> GetUserAsync(string Token)
    {
        var request = new RestRequest("/Auth/GetUser")
        {
            Authenticator = new JwtAuthenticator(Token)
        };

        var responseModel = await _client.GetAsync<GetUserResponseModel>(request);
        if (responseModel == null)
        {
            return ClientErrors.AccessUnauthorized;
        }
        return responseModel;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
